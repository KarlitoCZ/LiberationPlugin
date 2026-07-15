using System.Linq;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.Scp049Events;
using LabApi.Events.Arguments.Scp096Events;
using LabApi.Events.Arguments.Scp173Events;
using LabApi.Events.Arguments.Scp939Events;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using MapGeneration;
using PlayerRoles;
using PlayerRoles.FirstPersonControl.Spawnpoints;
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
            SpawnHandling.Instance.GiveLiberatorRole(ev.Player, LiberatorRank.Awakened);
            ev.IsAllowed = false;
            //ev.Player.DisarmedBy = null;
            ev.Player.IsDisarmed = false;

            if (RoleSpawnpointManager.TryGetSpawnpointForRole(RoleTypeId.NtfPrivate, out ISpawnpointHandler spawnpoint));
            spawnpoint.TryGetSpawnpoint(out Vector3 position, out float rot);
            ev.Player.Position = position;

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
                playerToRemove =  libPlayer;
                return;
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
        bool attackerScp = ev.Attacker.Role.GetTeam() == Team.SCPs;

        if (attackerScp && victimLiberator)
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
}
