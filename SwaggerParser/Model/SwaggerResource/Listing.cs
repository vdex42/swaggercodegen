namespace SwaggerParser.Model.SwaggerResource
{
    public class Listing
    {
        public string SwaggerVersion { get; set; }
        public string ApiVersion { get; set; }
        public SwaggerResource[] Apis { get; set; }
    }
}
