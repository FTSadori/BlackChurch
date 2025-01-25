using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.Mechanics.Inventory.Data;
using Client.Runtime.Game.ScriptableObjects;

namespace Client.Runtime.Game.Mechanics.Inventory
{
    public sealed class ItemDataCreator
    {
        static public ItemData GenerateItemData(MaterialScriptableObject itemObj, WholeInventoryHandler wholeInventoryHandler)
        {
            string lowerString = itemObj.rarity.ToString().ToLowerInvariant()[1..];

            ItemData itemData = new()
            {
                id = itemObj.id,
                nameId = "item." + itemObj.id,
                descriptionId = "item." + itemObj.id + ".description",
                typeId = ReturnItemTypeId(itemObj),
                rarityId = "rarity." + itemObj.rarity.ToString()[0] + lowerString,
                craftButtonActive = itemObj.craftsFromId1 != "" && itemObj.craftsFromId2 != "",
                useButtonActive = (itemObj.itemType != ItemType.MATERIAL) && wholeInventoryHandler.Contains(itemObj.id, 1),
                discardButtonActive = wholeInventoryHandler.Contains(itemObj.id, 1),
                leftMaterialId = itemObj.craftsFromId1,
                rightMaterialId = itemObj.craftsFromId2,
                canBeCraftedNow = wholeInventoryHandler.CanBeCrafted(itemObj.id),
            };
            return itemData;
        }

        static private string ReturnItemTypeId(MaterialScriptableObject itemObj)
        {
            switch (itemObj.itemType)
            {
                case ItemType.CONSUMABLE:
                    return "type.Consumable";
                case ItemType.MATERIAL:
                    return "type.Material";
                case ItemType.EQUIPABLE:
                    if (itemObj is EquipableScriptableObject equipableObj)
                    {
                        switch (equipableObj.equipableType)
                        {
                            case EquipableType.ACCESSORY:
                                return "type.Accessory";
                            case EquipableType.BODY:
                                return "type.Body";
                            case EquipableType.LEGS:
                                return "type.Legs";
                            case EquipableType.HEAD:
                                return "type.Head";
                            case EquipableType.WEAPON:
                                return equipableObj.weaponType switch
                                {
                                    WeaponType.MELEE => "type.MeleeWeapon",
                                    WeaponType.RANGED => "type.RangedWeapon",
                                    _ => "type.ShitError"
                                };
                        }
                    }
                break;
            }
            return "type.ShitError";
        }
    }
}