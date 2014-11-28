using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using NUnit.Framework;
using SwaggerParser.Services;
using SwaggerParser.Tests.Framework;

namespace SwaggerParser.Tests
{
    [TestFixture]
    public class SpecReaderTests
    {
        [Test]
        public void LoadSwaggerResourceShouldReturnSerialisedObject()
        {
            string testData = "{\"swaggerVersion\":\"1.2\",\"apiVersion\":\"1.0\",\"apis\":[{\"path\":\"/SomeEndpoint\"}]}";
            var responseMessage = new HttpResponseMessage();
            responseMessage.Content = new FakeHttpContent(testData);
            var messageHandler = new FakeHttpMessageHandler(responseMessage);
            var client = new HttpClient(messageHandler);
            var classUnderTest = new SpecReader(client);

            var response = classUnderTest.LoadSwaggerResource("http://someUrl/");
            Assert.That(response.ApiVersion, Is.EqualTo("1.0"));
            Assert.That(response.SwaggerVersion, Is.EqualTo("1.2"));
            Assert.That(response.Apis.First().Path, Is.EqualTo("/SomeEndpoint"));
        }

        [Test]
        public void LoadSwaggerApiDefinitionShouldReturnSerialisedObject()
        {

            string testData = "{\"swaggerVersion\":\"1.2\",\"apiVersion\":\"1.0\",\"basePath\":\"http://someUrl/\",\"resourcePath\":\"/SomeEndpoint\",\"apis\":[{\"path\":\"/someOperation\",\"operations\":[{\"method\":\"GET\",\"nickname\":\"GetSomething\",\"type\":\"array\",\"items\":{\"$ref\":\"someModel\"},\"parameters\":[{\"paramType\":\"path\",\"name\":\"customerId\",\"required\":true,\"type\":\"string\"}],\"responseMessages\":[]}]}],\"models\":{\"someModel\":{\"id\":\"someModel\",\"type\":\"object\",\"properties\":{\"Id\":{\"type\":\"string\"},\"Name\":{\"type\":\"string\"}},\"required\":[],\"subTypes\":[]}}}";
            var responseMessage = new HttpResponseMessage();
            responseMessage.Content = new FakeHttpContent(testData);
            var messageHandler = new FakeHttpMessageHandler(responseMessage);
            var client = new HttpClient(messageHandler);
            var classUnderTest = new SpecReader(client);

            var response = classUnderTest.LoadSwaggerApiDefinition("http://someUrl/SomeEndpoint");
            Assert.That(response.ApiVersion, Is.EqualTo("1.0"));
            Assert.That(response.SwaggerVersion, Is.EqualTo("1.2"));
            Assert.That(response.Apis.First().Path, Is.EqualTo("/someOperation"));
            Assert.That(response.Apis.First().Operations.Select(o => o.Method).First(), Is.EquivalentTo("GET"));
            Assert.That(response.Apis.First().Operations.Select(o => o.Type).First(), Is.EquivalentTo("array"));
            Assert.That(response.Apis.First().Operations.Select(o => o.Parameters.First().Name).First(), Is.EqualTo("customerId"));
            Assert.That(response.Apis.First().Operations.Select(o => o.Parameters.First().Type).First(), Is.EqualTo("string"));
        }
    }
}
