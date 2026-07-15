using CommandSystem;
using System;
using LabApi.Events.CustomHandlers;
using LabApi.Features;
using LabApi.Features.Enums;
using LabApi.Features.Wrappers;
using LabApi.Loader;
using LabApi.Loader.Features.Plugins;
using LiberationPlugin.Commands;
using RemoteAdmin;

namespace LiberationPlugin;


public class LiberationPlugin : Plugin<Config>
{
    public static Plugin Instance { get; set; } = null!;
    public static Config PluginConfig { get; set; } = null!;
    public override string Name { get; } = "Liberation Plugin";
    public override string Description { get; } = "This plugin does magic!";
    public override string Author { get; } = "Karlito";
    public override Version Version { get; } = new (0, 1, 0, 0);
    public override Version RequiredApiVersion { get; } = new (LabApiProperties.CompiledVersion);
    
    private static EventsHandler Events = new EventsHandler();
    
    public override void Enable()
    {
        Instance = this;
        PluginConfig = Config;
        CustomHandlersManager.RegisterEventsHandler(Events);
    }

    public override void Disable()
    {
        Instance = null;
        PluginConfig = null;
        CustomHandlersManager.UnregisterEventsHandler(Events);
    }

}