using BepInEx;
using BepInEx.Logging;

namespace CosmoDreadCoop;

[BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    public const string PLUGIN_GUID = "org.tontygh.plugins.cosmodreadcoop";
    public const string PLUGIN_NAME = "CosmoDread Co-Op";
    public const string PLUGIN_VERSION = "0.0.1.0";

    internal static new ManualLogSource Logger;
        
    private void Awake()
    {
        // Plugin startup logic
        Logger = base.Logger;
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
    }
}
