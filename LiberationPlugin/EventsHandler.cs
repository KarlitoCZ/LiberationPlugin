using System.Linq;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.Scp049Events;
using LabApi.Events.Arguments.Scp096Events;
using LabApi.Events.Arguments.Scp173Events;
using LabApi.Events.Arguments.Scp3114Events;
using LabApi.Events.Arguments.Scp939Events;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using MapGeneration;
using PlayerRoles;
using PlayerRoles.FirstPersonControl.Spawnpoints;
using PlayerRoles.PlayableScps.Scp106;
using UnityEngine;
using Random = System.Random;


namespace LiberationPlugin;

public class EventsHandler : CustomEventsHandler
{
    private bool IsLiberationPlayer(Player player)
    {
        return SpawnHandling.Instance.ActiveLiberationPlayers.Any(lp => lp.Player == player);
    }

    public override void OnPlayerEscaping(PlayerEscapingEventArgs ev)
    {
        var disarm = ev.Player.DisarmedBy;
        if (disarm == null) return;
        if (IsLiberationPlayer(disarm))
        {
            ev.Player.IsDisarmed = false;
            SpawnHandling.Instance.GiveLiberatorRole(ev.Player, LiberatorRank.All.ElementAt(0));
            ev.IsAllowed = false;
            
            Room outside = Room.List.FirstOrDefault(r => r.Name == RoomName.Outside);
            if (outside == null) return;
            var random = new Random();

            Vector3 localOffset = new Vector3(136.0f, -2.5f, -31.0f);
            Vector3 spawnPosition = outside.Transform.TransformPoint(localOffset);
            ev.Player.Position = spawnPosition +
                              new Vector3((float)random.NextDouble() - 0.5f, 0.0f, (float)random.NextDouble() - 0.5f);
        }
    }

    public override void OnPlayerChangedRole(PlayerChangedRoleEventArgs ev)
    {
        if (ev.NewRole.RoleTypeId == RoleTypeId.Tutorial) return;
        LiberatorPlayer? playerToRemove = null;
        foreach (var libPlayer in SpawnHandling.Instance.ActiveLiberationPlayers)
        {
            if (libPlayer.Player == ev.Player)
            {
                playerToRemove = libPlayer;
                break;
            }
        }

        if (playerToRemove != null)
        {
            SpawnHandling.Instance.CleanUpLiberatorRole(playerToRemove);
        }
    }

    public override void OnPlayerHurting(PlayerHurtingEventArgs ev)
    {
        if (ev.Attacker == null) return;

        bool victimLiberator = IsLiberationPlayer(ev.Player);
        bool attackerScp = ev.Attacker.IsSCP;
        bool attackerLiberator = IsLiberationPlayer(ev.Attacker);
        bool victimScp = ev.Player.IsSCP;

        if (attackerScp && victimLiberator)
        {
            ev.IsAllowed = false;
        }
        if (victimScp && attackerLiberator)
        {
            ev.IsAllowed = false;
        }
    }

    public override void OnScp049Attacking(Scp049AttackingEventArgs ev)
    {
        if (IsLiberationPlayer(ev.Target))
        {
            ev.IsAllowed = false;
        }
    }

    public override void OnScp049UsingSense(Scp049UsingSenseEventArgs ev)
    {
        if (IsLiberationPlayer(ev.Target))
        {
            ev.IsAllowed = false;
        }
    }

    public override void OnScp173AddingObserver(Scp173AddingObserverEventArgs ev)
    {
        if (IsLiberationPlayer(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }

    public override void OnScp173Snapping(Scp173SnappingEventArgs ev)
    {
        if (IsLiberationPlayer(ev.Target))
        {
            ev.IsAllowed = false;
        }
    }

    public override void OnScp939Attacking(Scp939AttackingEventArgs ev)
    {
        if (IsLiberationPlayer(ev.Target))
        {
            ev.IsAllowed = false;
        }
    }

    public override void OnScp096AddingTarget(Scp096AddingTargetEventArgs ev)
    {
        if (IsLiberationPlayer(ev.Target))
        {
            ev.IsAllowed = false;
        }
    }

    public override void OnScp3114StrangleStarting(Scp3114StrangleStartingEventArgs ev)
    {
        if (IsLiberationPlayer(ev.Target))
        {
            ev.IsAllowed = false;
        }
    }
    
}
