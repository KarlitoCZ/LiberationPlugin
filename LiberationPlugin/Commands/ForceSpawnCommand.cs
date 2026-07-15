using System;
using System.Diagnostics.CodeAnalysis;
using CommandSystem;

namespace LiberationPlugin.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class ForceSpawnCommand : ICommand
{

    public string Command { get; } = "lafspawn";
    public string[] Aliases { get; } = Array.Empty<string>();
    public string Description { get; } = "Force an L.A.F. spawn wave.";
    
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
    {
        var status = SpawnHandling.Instance.SpawnWave();
        if (status)
        {
            response = "Spawned";
        }
        else
        {
            response = "Spawn Failed";
        }
        
        return true;
    }

    public event EventHandler? CanExecuteChanged;
}