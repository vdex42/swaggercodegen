using System.Collections.Generic;

namespace SwaggerParser.Model.Api
{
    public class ApiSection
    {
        public string Name { get; set; }
        public IEnumerable<ApiMethod> Methods { get; set; }
    }
}
