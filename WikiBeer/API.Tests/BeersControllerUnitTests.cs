using AutoFixture;
using AutoFixture.Kernel;
using AutoMapper;
using FluentAssertions;
using Ipme.WikiBeer.API.Controllers;
using Ipme.WikiBeer.API.MapperProfiles;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Dtos.Ingredients;
using Ipme.WikiBeer.Entities;
using Ipme.WikiBeer.Entities.Ingredients;
using Ipme.WikiBeer.Persistance.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

/// <summary>
/// Pour donner l'accès à une classe internal voir les friends assemblies : 
/// https://docs.microsoft.com/en-us/dotnet/standard/assembly/friend
/// Note : On suppose dans ces test que Automappeur fonctionne correctement
/// On utilise Fluent assertions 
/// voir : https://fluentassertions.com/objectgraphs/ pour les détails d'utilisation
/// Discussion interessante sur des Test génériques : 
/// https://stackoverflow.com/questions/54048169/unit-test-with-generics-types
/// https://codinghelmet.com/articles/how-to-write-unit-tests-for-generic-classes
/// </summary>

namespace Ipme.WikiBeer.API.Tests
{
    [TestClass]
    public class BeersControllerUnitTests
    {
        private Fixture _fixture;
        private IMapper _mapper;

        private int _initBeersLength;
        private IEnumerable<BeerDto> BeersDto { get; set; }
        private IEnumerable<BeerEntity> BeersEntity { get; set; }
        private Mock<IGenericRepository<BeerEntity>> BeerRepository { get; set; }
        private BeersController BeersController { get; set;} 

        public BeersControllerUnitTests()
        {
            // Config fixture
            _fixture = new Fixture();
                // Type Hop à la place de type Ingrédient
            _fixture.Customizations.Add(new TypeRelay(typeof(IngredientEntity), typeof(HopEntity)));
            _fixture.Customizations.Add(new TypeRelay(typeof(IngredientDto), typeof(HopDto)));
            // Ignorer les références circulaires dans les Entities
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
                // Forcer autofixture à utiliser le constructeur le plus gourmand (pour remplir les property avec
                // setter privés)
            _fixture.Customize<BeerEntity>(c => c.FromFactory(
                new MethodInvoker(
                new GreedyConstructorQuery())));
            // Config mapper
            var configuration = new MapperConfiguration(cfg => cfg.AddMaps(typeof(DtoEntityProfile)));
            _mapper = new Mapper(configuration);
            // Magics
            _initBeersLength = 5;
        }

        [TestInitialize]
        public void InitTest()
        {
            // Remplissage des listes d'entities et dtos pour les test 
            BeersEntity = _fixture.CreateMany<BeerEntity>(_initBeersLength);
            BeersDto = _mapper.Map<IEnumerable<BeerDto>>(BeersEntity);
            BeerRepository = new Mock<IGenericRepository<BeerEntity>>();
            BeersController = new BeersController(BeerRepository.Object, _mapper); // Object donne l'instance dans le Mock Object
        }

        [TestMethod]
        public async Task Test_GetAllBeersAsync_Ok200()
        {
            //Arrange
            BeerRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(BeersEntity);

            //Act
            var result = await BeersController.GetAsync();

            //Assert (Status Code)
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            //Assert (Objet retourné)
            var dtos = okResult.Value as IEnumerable<BeerDto>;
            dtos.Should().NotBeNull();
            dtos.Count().Should().Be(_initBeersLength);
            dtos.Should().BeEquivalentTo(BeersDto);
            BeerRepository.Verify(repo => repo.GetAllAsync(), Times.Exactly(1));
        }

        [TestMethod]
        public async Task Test_GetAllBeersAsync_ServerError500()
        {
            //Arrange
            BeerRepository.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new Exception());

            //Act
            var result = await BeersController.GetAsync();

            //Assert (Status Code)
            var badResult = result.Result as StatusCodeResult;
            badResult.Should().NotBeNull();
            badResult.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }

        [TestMethod]
        public async Task Test_GetBeerByIdAsync_Ok200()
        {
            //Arrange
            var beerEntityToFind = BeersEntity.First();
            var guid = beerEntityToFind.Id;
            BeerRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(beerEntityToFind);
            var beerDtoToFind = _mapper.Map<BeerDto>(beerEntityToFind);
            
            //Act
            var result = await BeersController.GetAsync(guid);

            //Assert (Status Code)
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            //Assert (Objet retourné)
            var dto = okResult.Value as BeerDto;
            dto.Should().NotBeNull();
            dto.Should().BeEquivalentTo(beerDtoToFind);
            BeerRepository.Verify(repo => repo.GetByIdAsync(guid), Times.Exactly(1));
        }

        [TestMethod]
        public async Task Test_GetBeerByIdAsync_NotFound404()
        {
            //Arrange
            BeerRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((BeerEntity?)null);

            //Act
            var result = await BeersController.GetAsync(Guid.NewGuid());

            //Assert (Status Code)
            var badResult = result.Result as NotFoundResult;
            badResult.Should().NotBeNull();
            badResult.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task Test_GetBeerByIdAsync_ServerError500()
        {
            //Arrange
            BeerRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ThrowsAsync(new Exception());

            //Act
            var result = await BeersController.GetAsync(Guid.NewGuid());

            //Assert (Status Code)
            var badResult = result.Result as StatusCodeResult;
            badResult.Should().NotBeNull();
            badResult.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }

        [TestMethod]
        public async Task Test_PostAsync_Created201()
        {
            // Arrange 
            var new_beerDto = _fixture.Create<BeerDto>();
            var new_beerEntity = _mapper.Map<BeerEntity>(new_beerDto);
            BeerRepository.Setup(repo => repo.CreateAsync(It.IsAny<BeerEntity>())).ReturnsAsync(new_beerEntity);

            // Action 
            var result = await BeersController.PostAsync(new_beerDto);

            // Assert (Status Code)
            var createdResult = result as CreatedAtActionResult;
            createdResult.Should().NotBeNull();
            createdResult.StatusCode.Should().Be((int)HttpStatusCode.Created);
            //Assert (Objet retourné)
            var postedEntity = createdResult.Value as BeerDto;
            postedEntity.Should().NotBeNull();
            postedEntity.Should().BeEquivalentTo(new_beerDto);
        }

        [TestMethod]
        public async Task Test_PostAsync_ServerError500()
        {
            //Arrange
            var new_beerDto = _fixture.Create<BeerDto>();            
            BeerRepository.Setup(repo => repo.CreateAsync(It.IsAny<BeerEntity>())).ThrowsAsync(new Exception());

            //Act
            var result = await BeersController.PostAsync(new_beerDto);

            //Assert (Status Code)
            var badResult = result as StatusCodeResult;
            badResult.Should().NotBeNull();
            badResult.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }

        [TestMethod]
        public async Task Test_PutAsync_Ok200()
        {
            // Arrange 
            var new_beerDto = _fixture.Create<BeerDto>();
            var new_beerEntity = _mapper.Map<BeerEntity>(new_beerDto);
            BeerRepository.Setup(repo => repo.UpdateAsync(It.IsAny<BeerEntity>())).ReturnsAsync(new_beerEntity);

            // Action 
            var result = await BeersController.PutAsync(Guid.NewGuid(),new_beerDto);

            // Assert (Status Code)
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task Test_PutAsync_NotFound404()
        {
            //Arrange
            var new_beerDto = _fixture.Create<BeerDto>();
            BeerRepository.Setup(repo => repo.UpdateAsync(It.IsAny<BeerEntity>())).ReturnsAsync((BeerEntity?)null);

            //Act
            var result = await BeersController.PutAsync(Guid.NewGuid(), new_beerDto);

            //Assert (Status Code)
            var badResult = result as NotFoundResult;
            badResult.Should().NotBeNull();
            badResult.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task Test_PutAsync_ServerError500()
        {
            //Arrange
            var new_beerDto = _fixture.Create<BeerDto>();
            BeerRepository.Setup(repo => repo.UpdateAsync(It.IsAny<BeerEntity>())).ThrowsAsync(new Exception());

            //Act
            var result = await BeersController.PutAsync(Guid.NewGuid(), new_beerDto);

            //Assert (Status Code)
            var badResult = result as StatusCodeResult;
            badResult.Should().NotBeNull();
            badResult.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }
    }
}