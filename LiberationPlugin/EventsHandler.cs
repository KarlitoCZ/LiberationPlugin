using System.Linq;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.Scp049Events;
using LabApi.Events.Arguments.Scp096Events;
using LabApi.Events.Arguments.Scp173Events;
using LabApi.Events.Arguments.Scp939Events;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using PlayerRoles;


namespace LiberationPlugin;

public class EventsHandler : CustomEventsHandler
{
    private bool IsLiberationPlayer(Player player)
    {
        return SpawnHandling.Instance.ActiveLiberationPlayers.Any(lp => lp.Player == player);
    }

    public override void OnPlayerJoined(PlayerJoinedEventArgs ev)
    {
        
    }

    public override void OnPlayerChangedRole(PlayerChangedRoleEventArgs ev)
    {
        if (ev.NewRole.RoleTypeId == RoleTypeId.Tutorial) return;
        foreach (var libPlayer in SpawnHandling.Instance.ActiveLiberationPlayers)
        {
            if (libPlayer.Player == ev.Player)
            {
                SpawnHandling.Instance.CleanUpLiberatorRole(libPlayer);
            }
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
