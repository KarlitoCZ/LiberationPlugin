using Interactables.Interobjects.DoorUtils;
using InventorySystem;
using InventorySystem.Items;
using InventorySystem.Items.Keycards;
using LabApi.Features.Wrappers;
using MapGeneration;
using PlayerRoles;
using UnityEngine;
using KeycardItem = InventorySystem.Items.Keycards.KeycardItem;


namespace LiberationPlugin.Util;

public sealed class KeycardGiver
{
    public static KeycardGiver Instance { get; } = new KeycardGiver();
    private KeycardGiver() {}
    
    public Item GiveKeycard(Player player, ItemType type, string name, Color32 labelColor, int containment, int armory, int admin, Color32 permsColor, Color32 tintColor)
    {
        if (InventoryItemLoader.AvailableItems.TryGetValue(ItemType.KeycardCustomSite02, out ItemBase templateBase)
            && templateBase is KeycardItem template)
        {
            foreach (DetailBase detail in template.Details)
            {
                switch (detail)
                {
                    case CustomLabelDetail label:
                        label.SetArguments(new ArraySegment<object>(new object[]
                        {
                            name,
                            labelColor
                        }));
                        break;

                    case CustomPermsDetail perms:
                        perms.SetArguments(new ArraySegment<object>(new object[]
                        {
                            new KeycardLevels(containment, armory, admin),
                            permsColor
                        }));
                        break;

                    case CustomTintDetail tint:
                        tint.SetArguments(new ArraySegment<object>(new object[]
                        {
                            tintColor
                        }));
                        break;
                    case CustomItemNameDetail itemName:
                        itemName.SetArguments(new ArraySegment<object>(new object[]
                        {
                            name,
                        }));
                        break;
                    case NametagDetail nametag:
                        nametag.SetArguments(new ArraySegment<object>(new object[]
                        {
                            player.Nickname,
                        }));
                        break;
                }
            }
        }
        
        Item item = player.AddItem(ItemType.KeycardCustomSite02, ItemAddReason.StartingItem);
        return item;
    }
}