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

/// <summary>
/// Pour donner l'accès à une classe internal voir les friends assemblies : 
/// https://docs.microsoft.com/en-us/dotnet/standard/assembly/friend
/// Note : On suppose dans ces test que Automappeur fonctionne correctement
/// On utilise Fluent assertions 
/// voir : https://fluentassertions.com/objectgraphs/ pour les détails d'utilisation
/// </summary>
namespace API.Tests
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
        private BeersController BeersController { get; set; }

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
        public void Test_GetAllBeers_Ok200()
        {
            //Arrange
            BeerRepository.Setup(repo => repo.GetAll()).Returns(BeersEntity);

            //Act
            var result = BeersController.Get();

            //Assert (Status Code)
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            //Assert (Objet retourné)
            var dtos = okResult.Value as IEnumerable<BeerDto>;
            dtos.Should().NotBeNull();
            dtos.Count().Should().Be(_initBeersLength);
            dtos.Should().BeEquivalentTo(BeersDto);
            BeerRepository.Verify(repo => repo.GetAll(), Times.Exactly(1));
        }

        [TestMethod]
        public void Test_GetAllBeers_ServerError500()
        {
            //Arrange
            BeerRepository.Setup(repo => repo.GetAll()).Throws(new Exception());

            //Act
            var result = BeersController.Get();

            //Assert (Status Code)
            var badResult = result as StatusCodeResult;
            badResult.Should().NotBeNull();
            badResult.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }

        [TestMethod]
        public void Test_GetBeerById_Ok200()
        {
            //Arrange
            var beerEntityToFind = BeersEntity.First();
            var guid = beerEntityToFind.Id;
            BeerRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns(beerEntityToFind);
            var beerDtoToFind = _mapper.Map<BeerDto>(beerEntityToFind);

            //Act
            var result = BeersController.Get(guid);

            //Assert (Status Code)
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            //Assert (Objet retourné)
            var dto = okResult.Value as BeerDto;
            dto.Should().NotBeNull();
            dto.Should().BeEquivalentTo(beerDtoToFind);
            BeerRepository.Verify(repo => repo.GetById(guid), Times.Exactly(1));
        }

        [TestMethod]
        public void Test_GetBeerById_NotFound404()
        {
            //Arrange
            BeerRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns((BeerEntity?)null);

            //Act
            var result = BeersController.Get(Guid.NewGuid());

            //Assert (Status Code)
            var badResult = result as NotFoundResult;
            badResult.Should().NotBeNull();
            badResult.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void Test_GetBeerById_ServerError500()
        {
            //Arrange
            BeerRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).Throws(new Exception());

            //Act
            var result = BeersController.Get(Guid.NewGuid());

            //Assert (Status Code)
            var badResult = result as StatusCodeResult;
            badResult.Should().NotBeNull();
            badResult.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }

        [TestMethod]
        public void Test_Post_Created201()
        {
            // Arrange 
            var new_beerDto = _fixture.Create<BeerDto>();
            var new_beerEntity = _mapper.Map<BeerEntity>(new_beerDto);
            BeerRepository.Setup(repo => repo.Create(It.IsAny<BeerEntity>())).Returns(new_beerEntity);

            // Action 
            var result = BeersController.Post(new_beerDto);

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
        public void Test_Post_ServerError500()
        {
            //Arrange
            var new_beerDto = _fixture.Create<BeerDto>();
            BeerRepository.Setup(repo => repo.Create(It.IsAny<BeerEntity>())).Throws(new Exception());

            //Act
            var result = BeersController.Post(new_beerDto);

            //Assert (Status Code)
            var badResult = result as StatusCodeResult;
            badResult.Should().NotBeNull();
            badResult.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }

        [TestMethod]
        public void Test_Put_Ok200()
        {
            // Arrange 
            var new_beerDto = _fixture.Create<BeerDto>();
            var new_beerEntity = _mapper.Map<BeerEntity>(new_beerDto);
            BeerRepository.Setup(repo => repo.UpdateById(It.IsAny<Guid>(), It.IsAny<BeerEntity>())).Returns(new_beerEntity);

            // Action 
            var result = BeersController.Put(Guid.NewGuid(), new_beerDto);

            // Assert (Status Code)
            var okResult = result as OkResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [TestMethod]
        public void Test_Put_NotFound404()
        {
            //Arrange
            var new_beerDto = _fixture.Create<BeerDto>();
            BeerRepository.Setup(repo => repo.UpdateById(It.IsAny<Guid>(), It.IsAny<BeerEntity>())).Returns((BeerEntity?)null);

            //Act
            var result = BeersController.Put(Guid.NewGuid(), new_beerDto);

            //Assert (Status Code)
            var badResult = result as NotFoundResult;
            badResult.Should().NotBeNull();
            badResult.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void Test_Put_ServerError500()
        {
            //Arrange
            var new_beerDto = _fixture.Create<BeerDto>();
            BeerRepository.Setup(repo => repo.UpdateById(It.IsAny<Guid>(), It.IsAny<BeerEntity>())).Throws(new Exception());

            //Act
            var result = BeersController.Put(Guid.NewGuid(), new_beerDto);

            //Assert (Status Code)
            var badResult = result as StatusCodeResult;
            badResult.Should().NotBeNull();
            badResult.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }
    }
}