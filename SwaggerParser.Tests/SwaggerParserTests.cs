using Newtonsoft.Json.Linq;
using NUnit.Framework;
using SwaggerParser.Model.SwaggerApi;

namespace SwaggerParser.Tests
{
    [TestFixture]
    public class SwaggerParserTests
    {
        [Test]
        public void ParserShouldGenerateCode()
        {

            Listing spec = new Listing();   
            spec.Apis = new[] {new ApiObject
            {
                Path = "/someOperation",
                Operations = new OperationObject[]
                {
                    new OperationObject
                    {
                        Nickname = "doSomething",
                        Parameters = new ParameterObject[]
                        {
                            new ParameterObject
                            {
                                Name = "someRequiredParameter",
                                Type = "string",
                                Required = true
                            }, 
                            new ParameterObject
                            {
                                Name = "someOptionalParameter",
                                Type = "string",
                                Required = false
                            }, 
                        }
                    }, 
                }
            }};

            spec.Models =
                JObject.Parse(
                    "{\"someModel\":{\"id\":\"someModel\",\"type\":\"object\",\"properties\":{\"Id\":{\"type\":\"string\"},\"Name\":{\"type\":\"string\"}},\"required\":[],\"subTypes\":[]}}");

            Services.SwaggerParser classUnderTest = new Services.SwaggerParser();
            classUnderTest.ParseSpecDefinition("TestApi",spec);
        }

    }
}
