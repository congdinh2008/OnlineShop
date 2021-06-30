namespace OnlineShop.API.ConfigurationOptions
{
    public class CORS
    {
        public bool AllowAnyOrigin { get; set; }

        public string[] AllowedOrigins { get; set; }

        public string[] AllowedMethodsList { get; set; }

        public string[] AllowedHeadersList { get; set; }
    }
}
