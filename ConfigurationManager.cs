namespace EcommerceAPI;

public class ConfigurationManager
{
    private static readonly ConfigurationManager _instance = new ConfigurationManager();

    private ConfigurationManager()
    {
    }

    public static ConfigurationManager Instance => _instance;

    public string AppName { get; set; } = "My Application";
    public string Version { get; set; } = "1.0";
}
