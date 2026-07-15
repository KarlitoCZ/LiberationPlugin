using LabApi.Features.Wrappers;

namespace LiberationPlugin.Weapons;

public sealed class StunGun
{
    private List<Item> items = new();
    
    bool IsSelf(Item item)
    {
        return false;
    }
}