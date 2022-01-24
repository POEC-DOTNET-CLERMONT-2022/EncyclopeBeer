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

namespace API.Tests
{
    [TestClass]
    public class BeersControllerTest
    {
        private Fixture _fixture;
        private IMapper _mapper;

        private int _initBeersLength;
        private IEnumerable<BeerDto> BeersDto { get; set; }
        private Mock<IGenericRepository<BeerEntity>> BeerRepository { get; set; }
        private BeersController BeersController { get; set;} 

        public BeersControllerTest()
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
            //
            _initBeersLength = 5;
        }

        [TestInitialize]
        public void InitTest()
        {
            // Remplissage de la liste de dto pour le test 
            BeersDto = _fixture.CreateMany<BeerDto>(_initBeersLength);
            BeerRepository = new Mock<IGenericRepository<BeerEntity>>();
            BeersController = new BeersController(BeerRepository.Object, _mapper); // Object donne l'instance dans le Mock Object
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
            //var statusResult = result as StatusCodeResult;
            var statusResult = result as CreatedAtActionResult;
            statusResult.StatusCode.Should().Be((int)HttpStatusCode.Created);
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
            var statusResult = result as StatusCodeResult;
            //var statusResult = result as CreatedAtActionResult;
            statusResult.Should().NotBeNull();
            statusResult.Should().Be((int)HttpStatusCode.OK);
        }
    }
}