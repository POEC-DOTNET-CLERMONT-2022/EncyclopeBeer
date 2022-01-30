using AutoFixture;
using AutoFixture.Kernel;
using AutoMapper;
using FluentAssertions;
using Ipme.WikiBeer.API.Controllers;
using Ipme.WikiBeer.API.MapperProfiles;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Dtos.Ingredients;
using Ipme.WikiBeer.Entities;
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
            _fixture.Customizations.Add(new TypeRelay(typeof(IngredientDto), typeof(HopDto)));
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
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
        public void TestGetAllBeers_Ok()
        {
            //Arrange
            BeerRepository.Setup(repo => repo.GetAll()).Returns(BeersEntity);

            //Act
            var result = BeersController.Get();

            //Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            var dtos = okResult?.Value as IEnumerable<BeerDto>;
            dtos.Should().NotBeNull();
            dtos.Count().Should().Be(_initBeersLength);
            dtos.Should().BeEquivalentTo(BeersDto);
            BeerRepository.Verify(repo => repo.GetAll(), Times.Exactly(1));
        }

        [TestMethod]
        public void TestGetBeerById_Ok()
        {
            //Arrange
            var beerEntityToFind = BeersEntity.First();
            var guid = beerEntityToFind.Id;
            BeerRepository.Setup(repo => repo.GetById(guid)).Returns(beerEntityToFind);
            var beerDtoToFind = _mapper.Map<BeerDto>(beerEntityToFind);
            
            //Act
            var result = BeersController.Get(guid);

            //Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            var dto = okResult?.Value as BeerDto;
            dto.Should().NotBeNull();
            dto.Should().BeEquivalentTo(beerDtoToFind);
            BeerRepository.Verify(repo => repo.GetById(guid), Times.Exactly(1));
        }

        [TestMethod]
        public void Test_Post_Ok()
        {
            // Arrange 
            var new_beerDto = _fixture.Create<BeerDto>();
            var new_beerEntity = _mapper.Map<BeerEntity>(new_beerDto);
            BeerRepository.Setup(repo => repo.Create(It.IsAny<BeerEntity>())).Returns(new_beerEntity);

            // Action 
            var result = BeersController.Post(new_beerDto);

            // Assert
            var createdResult = result as CreatedAtActionResult;
            createdResult.Should().NotBeNull();
            createdResult.StatusCode.Should().Be((int)HttpStatusCode.Created);
            var postedEntity = createdResult.Value as BeerEntity;
            postedEntity.Should().NotBeNull();
            postedEntity.Should().BeEquivalentTo(new_beerEntity);
        }

        [TestMethod]
        public void Test_Put_Ok()
        {
            // Arrange 
            var new_beerDto = _fixture.Create<BeerDto>();
            var new_beerEntity = _mapper.Map<BeerEntity>(new_beerDto);
            BeerRepository.Setup(repo => repo.UpdateById(It.IsAny<Guid>(),It.IsAny<BeerEntity>())).Returns(new_beerEntity);

            // Action 
            var result = BeersController.Put(Guid.NewGuid(),new_beerDto);

            // Assert
            var okResult = result as OkResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
    }
}