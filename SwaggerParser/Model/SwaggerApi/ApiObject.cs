namespace SwaggerParser.Model.SwaggerApi
{
    public class ApiObject
    {
        public string Path { get; set; }
        public string Description { get; set; }
        public OperationObject[] Operations { get; set; }
    }
}
