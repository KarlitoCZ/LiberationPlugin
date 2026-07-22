using System;
using System.Reflection;
using LabApi.Events.CustomHandlers;
using LabApi.Features;
using LabApi.Features.Console;
using LabApi.Loader;
using LabApi.Loader.Features.Plugins;
using LiberationPlugin.Util;

namespace LiberationPlugin;



public class LiberationPlugin : Plugin<PluginConfig>
{
    public static Plugin Instance { get; set; } = null!;
    public static PluginConfig PluginConfig { get; set; } = null!;
    public override string Name { get; } = "Liberation Plugin";
    public override string Description { get; } = "Add The Liberators to your SCP:SL server";
    public override string Author { get; } = "Karlito";
    public override Version Version { get; } = new (1, 1, 0, 0);
    public override Version RequiredApiVersion { get; } = new (LabApiProperties.CompiledVersion);
    //public override string ConfigFileName { get; set; } = "config.yml";
    
    private static EventsHandler Events = new();
    
    public override void Enable()
    {
        Instance = this;
        PluginConfig = Config;
        CustomHandlersManager.RegisterEventsHandler(Events);
        SpawnHandling.Instance.StartWatcher();
        SaveConfig();
        
        bool isTimerApiAvailable = PluginLoader.Plugins.Values.Any(p => p.GetName().Name.Equals("RespawnTimer", StringComparison.OrdinalIgnoreCase));
        
        if (isTimerApiAvailable)
        {
            RespawnTimer.Enable();
            Logger.Info("RespawnTimer.dll found");
        }
        else
        {
            Logger.Info("RespawnTimer.dll not found");
        }
    }

    public override void Disable()
    {
        Instance = null;
        PluginConfig = null;
        CustomHandlersManager.UnregisterEventsHandler(Events);
        Weapon.ClearAll();
        SpawnHandling.Instance.StopWatcher();
    }

}