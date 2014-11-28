using System;
using System.Net.Http;
using SwaggerParser.Model.SwaggerResource;
using SwaggerParser.Services;

namespace GenerateCodeFromSwagger
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: GenerateCodeFromSwagger.exe http://urlToSwaggerDef");
                return;
            }

            using (HttpClient client = new HttpClient())
            {
                Console.WriteLine("Loading api listing from {0}", args[0]);
                SpecReader specReader = new SpecReader(client);
                Listing listing = specReader.LoadSwaggerResource(args[0]);
                foreach (var api in listing.Apis)
                {
                    Console.WriteLine("Loading api definition from {0}", api.Path);
                    SwaggerParser.Model.SwaggerApi.Listing apiSpec = specReader.LoadSwaggerApiDefinition(api.Path);
                    

                }
                

            }
            

        }
    }
}
