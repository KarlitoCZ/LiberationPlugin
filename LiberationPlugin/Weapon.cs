using InventorySystem;
using InventorySystem.Items;
using LabApi.Features.Wrappers;
using PlayerStatsSystem;

namespace LiberationPlugin;

public abstract class Weapon
{
    public abstract ItemType ItemType { get; }

    private ItemBase item;

    private static readonly Dictionary<ushort, Weapon> SerialRegistry = new();

    public static void Register(ushort serial, Weapon weapon) => SerialRegistry[serial] = weapon;
    public static void Unregister(ushort serial) => SerialRegistry.Remove(serial);
    
    public static void ClearAll() => SerialRegistry.Clear();
    
    public static Weapon? Get(ushort serial)
    {
        if (SerialRegistry.TryGetValue(serial, out var weapon))
            return weapon;
        return null;
    }

    public void Give(Player player)
    {
        var itemBase = player.Inventory.ServerAddItem(ItemType.GunRevolver, ItemAddReason.StartingItem, (ushort)new Random().Next(0,99999999));
        item = itemBase;
        Register(itemBase.ItemId.SerialNumber, this);
    }

    public virtual void OnShooting(Player player) { }
    public virtual void OnShot(Player player) { }
    public virtual void OnHurting(Player attacker, Player target, DamageHandlerBase handler, ref bool isAllowed) { }
    public virtual void OnReloading(Player player) { }
    public virtual void OnReloaded(Player player) { }
    public virtual void OnDryFiring(Player player) { }
    public virtual void OnAimed(Player player) { }
    public virtual void OnEquipped(Player player) { }
    public virtual void OnHolstered(Player player) { }
    public virtual void OnPickedUp(Player player) { }
    public virtual void OnDropped(Player player) { }
}
