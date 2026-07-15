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

    public override void OnScp049Attacking(Scp049AttackingEventArgs ev)
    {
        base.OnScp049Attacking(ev);
    }

    public override void OnScp049UsingSense(Scp049UsingSenseEventArgs ev)
    {
        base.OnScp049UsingSense(ev);
    }

    public override void OnScp173AddingObserver(Scp173AddingObserverEventArgs ev)
    {
        base.OnScp173AddingObserver(ev);
    }

    public override void OnScp173Snapping(Scp173SnappingEventArgs ev)
    {
        base.OnScp173Snapping(ev);
    }

    public override void OnScp939Attacking(Scp939AttackingEventArgs ev)
    {
        base.OnScp939Attacking(ev);
    }
    
    

    public override void OnScp096AddingTarget(Scp096AddingTargetEventArgs ev)
    {
        if (ev.Target.Role != RoleTypeId.Tutorial) return;
        ev.IsAllowed = false;
    }
}
