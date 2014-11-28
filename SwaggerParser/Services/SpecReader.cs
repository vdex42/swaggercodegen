using System;
using System.Net.Http;
using SwaggerParser.Model.SwaggerApi;

namespace SwaggerParser.Services
{
   public class SpecReader
    {
       private readonly HttpClient _httpClient;

       public SpecReader(HttpClient httpClient)
       {
           _httpClient = httpClient;
       }

       public Model.SwaggerResource.Listing LoadSwaggerResource(string url)
       {
           var response = _httpClient.GetAsync(url).Result;

           if (!response.IsSuccessStatusCode)
           {
               throw new Exception(string.Format("Error reading swagger resource - {0} {1}", response.StatusCode,
                   response.Content.ReadAsStringAsync().Result));
           }
           string responseString = response.Content.ReadAsStringAsync().Result;
           return Newtonsoft.Json.JsonConvert.DeserializeObject<Model.SwaggerResource.Listing>(responseString);
       }

       public Listing LoadSwaggerApiDefinition(string url)
       {
           var response = _httpClient.GetAsync(url).Result;

           if (!response.IsSuccessStatusCode)
           {
               throw new Exception(string.Format("Error reading swagger api definition - {0} {1}", response.StatusCode,
                   response.Content.ReadAsStringAsync().Result));
           }
           string responseString = response.Content.ReadAsStringAsync().Result;
           Listing loadSwaggerApiDefinition = Newtonsoft.Json.JsonConvert.DeserializeObject<Listing>(responseString);
           
           return loadSwaggerApiDefinition;
       }


    }
}
