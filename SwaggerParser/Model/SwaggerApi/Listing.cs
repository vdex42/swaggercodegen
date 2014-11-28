using Newtonsoft.Json.Linq;

namespace SwaggerParser.Model.SwaggerApi
{
   public class Listing
    {
       public string SwaggerVersion { get; set; }
       public string ApiVersion { get; set; }
       public string BasePath { get; set; }
       public string ResourcePath { get; set; }
       public ApiObject[] Apis { get; set; }
       public JObject Models { get; set; }       
        /*
         * TODO:
         * produces
         * consumes
         * authorizations
         */
    }
}
