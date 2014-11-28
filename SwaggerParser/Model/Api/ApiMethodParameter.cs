using SwaggerParser.Model.SwaggerApi;

namespace SwaggerParser.Model.Api
{
    public class ApiMethodParameter
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public bool Required { get; set; }

        public ParamTypes ParamType { get; set; }
    }
}
