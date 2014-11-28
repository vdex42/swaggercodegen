using System.Collections.Generic;
using System.Linq;
using SwaggerParser.Model.Api;
using SwaggerParser.Model.SwaggerApi;

namespace SwaggerParser.Services
{
    public class ConvertApiSpecToCode
    {

        public static ApiDefinition GenerateApiDefinition(string apiName, Listing apiListing)
        {
            ApiDefinition definition= new ApiDefinition
            {
                Name = apiName,
                Sections = apiListing.Apis
                    .Select(api => new ApiSection
                    {
                        Name = ConvertPathToName(api.Path),
                        Methods = api.Operations.Select(operation => new ApiMethod
                        {
                            Name = operation.Nickname,                            
                            ReturnType = ConvertToCSharpType(operation.Type),
                            Parameters = operation.Parameters.Where(p => p.Required).Select(CreateApiParameter).ToList(),
                            Path =  apiListing.ResourcePath + api.Path
                        })
                    })
            };

            return definition;
        }

        private static ApiMethodParameter CreateApiParameter(ParameterObject param)
        {
            return new ApiMethodParameter
            {
                Name = param.Name, 
                Type = ConvertToCSharpType(param.Type),
                Required = param.Required,
                ParamType = param.ParamType
            };
        }

        private static string ConvertPathToName(string path)
        {
            return path.Replace("/", "");
        }



        private static string ConvertToCSharpType(string type)
        {
            return type;
        }
    }
}
