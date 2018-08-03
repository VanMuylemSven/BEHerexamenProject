using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using DataGent.Models;
using DataGent.Services;
using DataGent.Repositories;
using DataGent.Web.Controllers;
using System.Diagnostics;

namespace DataGent.Tests
{
    public class CommentaarTest
    {

        //Calls the Create & Delete methods of the repository, checks if they've been called and return a result
        [Fact]
        public void CreateCommentaar_Should_Call_Post()
        {
            //arrange
            Commentaar commentaar = new Commentaar
            {
                CommentaarId = 0,
                StadId = 0,
                UserId = "bdadf837-52b1-43b7-8cf2-5729bdbed093",
                CommentaarText = "text",
                Tijdstip = DateTime.Now
            };
            var mockRepository = new Mock<IDataGentRepoAsync>();
            var service = new DataGentService(mockRepository.Object);

            //act
            var result = service.PostCommentaar(commentaar);

            //assert
            mockRepository.Verify(v => v.PostCommentaar(commentaar), Times.Once());
            //Assert.NotNull(result);
        }
        [Fact]
        public void DeleteCommentaar_Should_Call_Delete()
        {
            //arrange
            Commentaar commentaar = new Commentaar
            {
                CommentaarId = 0,
                StadId = 0,
                UserId = "bdadf837-52b1-43b7-8cf2-5729bdbed093",
                CommentaarText = "text",
                Tijdstip = DateTime.Now
            };


            var mockRepository = new Mock<IDataGentRepoAsync>();
            var service = new DataGentService(mockRepository.Object);

            //act
            var result = service.DeleteCommentaar(commentaar);

            //assert
            //Assert.NotNull(result);
            mockRepository.Verify(v => v.DeleteCommentaar(commentaar), Times.Once());
        }

        [Fact]
        public void DataGentService_Should_Return_StedenList()
        {
            var stedenList = new List<Stad>
            {
                new Stad
                {
                    Id = 0,
                    Naam = "Roomer"
                },
                new Stad
                {
                    Id = 1,
                    Naam = "Achteruit"
                },
                new Stad
                {
                    Id = 2,
                    Naam = "Westies"
                },
            };

            var mockRepository = new Mock<IDataGentRepoAsync>();
            var service = new Mock<IDataGentService>();

            //act
            service.Setup(data => data.GetDataGentFromURL()).Returns(stedenList);

            //var controller = new DataGentController(service.Object, null, null);
            //var result = controller.Index();
            var result = service.Object.GetDataGentFromURL();

            //assert
            Assert.Equal(3, result.Count());
        }

        /*[Fact]
        public void Index_Should_Display_StedenList()
        {
            var stedenList = new List<Stad>
            {
                new Stad
                {
                    Id = 0,
                    Naam = "Roomer"
                },
                new Stad
                {
                    Id = 1,
                    Naam = "Achteruit"
                },
                new Stad
                {
                    Id = 2,
                    Naam = "Westies"
                },
            };

            var mockService = new Mock<IDataGentService>();
            mockService.Setup(b => b.GetDataGentFromURL()).Returns(stedenList);

            var controller = new DataGentController(mockService.Object, null, null);
            var result = controller.Index() as ViewResult; //////////////

            //Assert.IsNotNull(result);
            //Assert.True(result.ViewData.Values.Count == mockService.Object.GetDataGentFromURL().Count()); //////////////

        }*/
    }
}
