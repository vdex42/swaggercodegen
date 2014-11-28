namespace SwaggerParser.Model.SwaggerApi
{
    public class OperationObject
    {
        public string Method { get; set; }
        public ParameterObject[] Parameters { get; set; }        
        public string Nickname { get; set; }
        public string Type { get; set; }
        
    }
}
