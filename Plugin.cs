using BepInEx;
using CosmoDreadCoop.Utils;

namespace CosmoDreadCoop;

[BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    public const string PLUGIN_GUID = "org.tontygh.plugins.cosmodreadcoop";
    public const string PLUGIN_NAME = "CosmoDread Co-Op";
    public const string PLUGIN_VERSION = "0.1.0";

    public const int APP_ID = 1256060;
    
    private void Awake()
    {
        // Plugin startup logic
        CosmoLogger.SetSource(Logger); // We need to do this first cause we log stuff everywhere unpredictably
        PersistentData.Initialize(); // We also need to do this first because stuff might generate data
        Initialize();
        CosmoLogger.LogInfo($"Loaded {PLUGIN_GUID} v{PLUGIN_VERSION}");
    }

    private void OnDestroy()
    {
        // Shut down steamworks
        Steamworks.SteamClient.Shutdown();
        SteamAPILoader.FreeSteamAPI();
    }

    private void Initialize()
    {
        // Prepare steamworks
        SteamAPILoader.LoadSteamAPI();
        Steamworks.SteamClient.Init(APP_ID, true);
    }
}
