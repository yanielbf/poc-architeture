namespace WROBoxLabelGeneration.SharedKernel.Configurations
{
    public class AppSettings
    {
        public string Environment { get; set; }
        public string ApplicationName { get; set; }
        public string APPINSIGHTS_INSTRUMENTATIONKEY { get; set; }
        public AuthClientSettings AuthClientSettings { get; set; }
        public ConnectionStringsSettings ConnectionStringsSettings { get; set; }
        public HttpClientSettings HttpClientSettings { get; set; }

        public LibraryInfrastructure LibraryInfrastructure { get; set; }

    }

    public class AuthClientSettings
    {
        public string AuthAddress { get; set; }
        public string AuthClientId { get; set; }
        public string AuthClientSecret { get; set; }
        public string AuthGrantType { get; set; }
    }

    public class ConnectionStringsSettings
    {
        public string ShipbobLive { get; set; }
    }

    public class HttpClientSettings
    {
        public ApiSettings LabelingApi { get; set; }
        public ApiSettings AttachmentApi { get; set; }

        public string Scopes()
        {
            return $"{LabelingApi.Scopes} {AttachmentApi.Scopes}";
        }
    }

    public class ApiSettings
    {
        public string BaseUrl { get; set; }
        public string Scopes { get; set; }
    }

    public class LibraryInfrastructure { 
    
        public string PdfGenerator { get; set; }
    }

}
