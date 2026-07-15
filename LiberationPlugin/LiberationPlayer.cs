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

    public static readonly LiberatorRank Awakened = new (0, 15,"Awakened", new List<ItemType>
    {
        ItemType.Adrenaline,
        ItemType.Medkit,
        ItemType.ArmorCombat,
        ItemType.GunFSP9,
        ItemType.GrenadeFlash
    });
    public static readonly LiberatorRank Freeborn = new (1, 10,"Freeborn", new List<ItemType>
    {
        ItemType.Adrenaline,
        ItemType.Medkit,
        ItemType.ArmorCombat,
        ItemType.GunFSP9,
        ItemType.GrenadeFlash
    });
    public static readonly LiberatorRank Oathkeeper = new (2, 5,"Oathkeeper", new List<ItemType>()
    {
        ItemType.Adrenaline,
        ItemType.Medkit,
        ItemType.ArmorCombat,
        ItemType.GunFSP9,
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