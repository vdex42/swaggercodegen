using System.Collections.Generic;

namespace SwaggerParser.Model.Api
{
    public class ApiMethod
    {
        public string ReturnType { get; set; }
        public string Name { get; set; }
        public List<ApiMethodParameter> Parameters { get; set; }

        public string Path { get; set; }
    }
}
