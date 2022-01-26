using AutoFixture;
using AutoFixture.Kernel;
using AutoMapper;
using FluentAssertions;
using Ipme.WikiBeer.API.Controllers;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Entities;
using Ipme.WikiBeer.Entities.Ingredients;
using Ipme.WikiBeer.Persistance.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Test
{
    [TestClass]
    public class MoqBeerControllerTest
    {
        private BeersController BeerController { get; set; }

        public Mock<IGenericRepository<BeerEntity>> BeerRepository { get; set; }

        public IMapper Mapper { get; set; }

        public ILogger Logger { get; set; } = new NullLogger<BeersController>();

        public Fixture Fixture { get; set; }

        private IEnumerable<BeerEntity> Beers { get; set; }

        private BeerEntity Beer { get; set; }   

        public MoqBeerControllerTest()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddMaps(typeof(BeersController)));
            Mapper = new Mapper(configuration);
        }

        [TestInitialize]
        public void InitTest()
        {
            Fixture = new Fixture();
            Fixture.Customizations.Add(new TypeRelay(typeof(IngredientEntity), typeof(HopEntity)));
            Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => Fixture.Behaviors.Remove(b));
            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            Beers = Fixture.CreateMany<BeerEntity>(10);
            BeerRepository = new Mock<IGenericRepository<BeerEntity>>();
            BeerController = new BeersController(BeerRepository.Object, Mapper);
        }

        [TestMethod]
        public void TestGetAllBeers_Ok()
        {
            //Arrange
            BeerRepository.Setup(repo => repo.GetAll()).Returns(Beers);

            //Act
            var result = BeerController.Get();

            //Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            var entities = okResult?.Value as IEnumerable<BeerDto>;
            entities.Should().NotBeNull();
            entities.Count().Should().Be(10);

            BeerRepository.Verify(repo => repo.GetAll(), Times.Exactly(1));
        }

        
        [TestMethod]
        public void TestGetBeerById_Ok()
        {
            //Arrange
            var beerToFind = Beers.First();
            Beer = beerToFind;
            var guid = beerToFind.Id;
            BeerRepository.Setup(repo => repo.GetById(guid)).Returns(Beer);

            //Act
            var result = BeerController.Get(guid);

            //Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            var entities = okResult?.Value as BeerDto;
            entities.Should().NotBeNull();

            BeerRepository.Verify(repo => repo.GetById(guid), Times.Exactly(1));
        }
    }
}