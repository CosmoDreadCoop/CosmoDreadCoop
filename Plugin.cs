using BepInEx;

namespace CosmoDreadCoop;

[BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    public const string PLUGIN_GUID = "org.tontygh.plugins.cosmodreadcoop";
    public const string PLUGIN_NAME = "CosmoDread Co-Op";
    public const string PLUGIN_VERSION = "0.1.0";
        
    private void Awake()
    {
        // Plugin startup logic
        CosmoLogger.SetSource(Logger);
        CosmoLogger.LogInfo($"Loaded {PLUGIN_GUID} v{PLUGIN_VERSION}");
    }
}
