using System.Collections.Generic;

namespace SwaggerParser.Model.Api
{
    public class ApiDefinition
    {
        public string Name { get;set;}
        public IEnumerable<ApiSection> Sections { get; set; }
    }
}
