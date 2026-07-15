using System;
using System.Diagnostics.CodeAnalysis;
using CommandSystem;

namespace LiberationPlugin.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class ForceSpawnCommand : ICommand
{

    public string Command { get; } = "lafspawn";
    public string[] Aliases { get; } = Array.Empty<string>();
    public string Description { get; } = "Force a LAF. spawn wave.";
    
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
    {
        SpawnHandling.Instance.SpawnWave();
        response = "Spawned";
        return true;
    }

    public event EventHandler? CanExecuteChanged;
}