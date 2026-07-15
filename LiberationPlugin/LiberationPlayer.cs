using System.Collections.Generic;
using LabApi.Features.Wrappers;

namespace LiberationPlugin;

public class LiberatorRank
{
    public string Name { get; }
    public int Value { get; }
    
    public int PlayerCap { get; }
    
    public List<ItemType> Loadout { get; }
    

    private LiberatorRank(int value, int playerCap ,string name, List<ItemType> loadout)
    {
        Value = value;
        Name = name;
        Loadout = loadout;
    }

    public static readonly LiberatorRank Awakened = new (1, 15,"Awakened", new List<ItemType>
    {
        ItemType.Adrenaline,
        ItemType.Medkit,
        ItemType.ArmorCombat,
        ItemType.GunE11SR,
        ItemType.GrenadeFlash
    });
    public static readonly LiberatorRank Freeborn = new (2, 10,"Freeborn", new List<ItemType>
    {
        ItemType.Adrenaline,
        ItemType.Medkit,
        ItemType.ArmorCombat,
        ItemType.GunE11SR,
        ItemType.GrenadeFlash
    });
    public static readonly LiberatorRank Oathkeeper = new (3, 3,"Oathkeeper", new List<ItemType>()
    {
        ItemType.Adrenaline,
        ItemType.Medkit,
        ItemType.ArmorCombat,
        ItemType.GunE11SR,
        ItemType.GrenadeFlash
    });

    public static IEnumerable<LiberatorRank> All => new[] { Awakened, Freeborn, Oathkeeper };

    public override string ToString() => Name;
}

public class LiberatorPlayer(Player player, LiberatorRank rank)
{
    public Player Player = player;
    public LiberatorRank Rank = rank;
}