
using LabApi.Features.Wrappers;
using PlayerRoles.Spectating;
using RespawnTimer.API;

namespace LiberationPlugin;

internal static class RespawnTimer
{

    public static void Enable()
    {
        
        TimerAPI.RegisterProperty("liberation_role", player => GetPublicRoleName(player));
    }
    
    public static string GetPublicRoleName(Player player)
    {
        
        var libPlayer = SpawnHandling.Instance.GetLiberationPlayer(player);
        if (libPlayer == null) return "";

        return "LAF. - " + libPlayer.Rank.Name;
    }
}