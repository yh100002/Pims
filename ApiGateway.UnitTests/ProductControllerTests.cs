using System;
using Xunit;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Utils.Http;
using ApiGateway.Controllers;
using ApiGateway.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiGateway.UnitTests
{
    public class ProductControllerTests
    {
        [Fact]
        public async Task GetProducts_WithSomeRelervantProductData_ReturnsActionResults()
        {
            var settingsMock = new Mock<IOptions<PimsSettings>>();
            settingsMock.Setup(s => s.Value).Returns(new PimsSettings{ ProductQueryApiUrl = "Some API" });                       
            var apiClientMock = new Mock<IHttpClient>();
            apiClientMock.Setup(s => s.GetStringAsync(It.IsAny<string>())).ReturnsAsync("Some Response");   

            var target = BuilTarget(settingsMock.Object, apiClientMock.Object);
            var result = await target.GetProducts(1, 20);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var objects = okResult.Value.Should().BeAssignableTo<string>().Subject;
            objects.Should().Be("Some Response");
        }

        [Fact]
        public async Task GetProducts_WithSomeException_ReturnsActionResults()
        {
            var settingsMock = new Mock<IOptions<PimsSettings>>();
            settingsMock.Setup(s => s.Value).Returns(new PimsSettings{ ProductQueryApiUrl = "Some API" });                       
            var apiClientMock = new Mock<IHttpClient>();
            apiClientMock.Setup(s => s.GetStringAsync(It.IsAny<string>())).Throws(new Exception("Some exception"));

            var target = BuilTarget(settingsMock.Object, apiClientMock.Object);           

            Exception ex = await Assert.ThrowsAsync<Exception>(() => target.GetProducts(1, 20));
            Assert.Equal(ex.Message, "Some exception");
        }

        [Fact]
        public async Task GetProduct_WithSomeRelervantProductData_ReturnsActionResults()
        {
            var settingsMock = new Mock<IOptions<PimsSettings>>();
            settingsMock.Setup(s => s.Value).Returns(new PimsSettings{ ProductQueryApiUrl = "Some API" });                       
            var apiClientMock = new Mock<IHttpClient>();
            apiClientMock.Setup(s => s.GetStringAsync(It.IsAny<string>())).ReturnsAsync("Some Response");   

            var target = BuilTarget(settingsMock.Object, apiClientMock.Object);
            var result = await target.GetProduct("Some Id");

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var objects = okResult.Value.Should().BeAssignableTo<string>().Subject;
            objects.Should().Be("Some Response");
        }

        [Fact]
        public async Task GetProduct_WithSomeException_ReturnsActionResults()
        {
            var settingsMock = new Mock<IOptions<PimsSettings>>();
            settingsMock.Setup(s => s.Value).Returns(new PimsSettings{ ProductQueryApiUrl = "Some API" });                       
            var apiClientMock = new Mock<IHttpClient>();
            apiClientMock.Setup(s => s.GetStringAsync(It.IsAny<string>())).Throws(new Exception("Some exception"));

            var target = BuilTarget(settingsMock.Object, apiClientMock.Object);           

            Exception ex = await Assert.ThrowsAsync<Exception>(() => target.GetProduct("Some Id"));
            Assert.Equal(ex.Message, "Some exception");
        }



        private static ProductController BuilTarget(IOptions<PimsSettings> pimsSettings, IHttpClient apiClient)
        {            
            return new ProductController(pimsSettings, apiClient);
        }


    }
}
