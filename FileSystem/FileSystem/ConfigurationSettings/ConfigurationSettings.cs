namespace FileSystem.ConfigurationSettings
{
    public static class ConfigurationSettings
    {
        public static IConfiguration GetConfig(bool isDevelopment)
        {
            return isDevelopment ? new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build()
            :
            new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();
        }
    }
}
