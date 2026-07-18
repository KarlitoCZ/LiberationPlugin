using System;
using LabApi.Events.CustomHandlers;
using LabApi.Features;
using LabApi.Features.Console;
using LabApi.Loader.Features.Plugins;

namespace LiberationPlugin;


public class LiberationPlugin : Plugin<PluginConfig>
{
    public static Plugin Instance { get; set; } = null!;
    public static PluginConfig PluginConfig { get; set; } = null!;
    public override string Name { get; } = "Liberation Plugin";
    public override string Description { get; } = "Add The Liberators to your SCP:SL server";
    public override string Author { get; } = "Karlito";
    public override Version Version { get; } = new (0, 4, 0, 0);
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