using CustomPlayerEffects;
using LabApi.Features.Wrappers;
using PlayerStatsSystem;

namespace LiberationPlugin.Weapons;

public sealed class StunGun : Weapon
{
    public override ItemType ItemType => ItemType.GunRevolver;
    private float _duration = LiberationPlugin.PluginConfig.StunDuration;

    public override void OnHurting(Player attacker, Player target, DamageHandlerBase handler, ref bool isAllowed)
    {
        target.EnableEffect<Blurred>(10, _duration, false);
        target.EnableEffect<Slowness>(100, _duration, false);
        target.EnableEffect<Blindness>(50, _duration, false);
        target.CurrentItem = null;
        attacker.SendHitMarker();
        isAllowed = false;
    }

    public override void OnEquipped(Player player)
    {
        player.SendHint($"<color=#fcba03>Stun Gun</color>\nStuns players for {(int)_duration} seconds", 2.5f);
    }
}
