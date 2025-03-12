using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.Mechanics.Inventory.Data;
using Client.Runtime.Game.ScriptableObjects;
using Client.Runtime.Game.ScriptableObjects.Items;

namespace Client.Runtime.Game.Mechanics.Inventory
{
    public sealed class TypeIds
    {
        static public readonly string Consumable = "type.Consumable";
        static public readonly string Material = "type.Material";
        static public readonly string Accessory = "type.Accessory";
        static public readonly string Body = "type.Body";
        static public readonly string Legs = "type.Legs";
        static public readonly string Head = "type.Head";
        static public readonly string Melee = "type.MeleeWeapon";
        static public readonly string Ranged = "type.RangedWeapon";
        static public readonly string Error = "type.Error";

        static public bool IsEquipable(string id)
        {
            return (id != Consumable) && (id != Material) && (id != Error);
        }
    }

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
                    return TypeIds.Consumable;
                case ItemType.MATERIAL:
                    return TypeIds.Material;
                case ItemType.EQUIPABLE:
                    if (itemObj is EquipableScriptableObject equipableObj)
                    {
                        switch (equipableObj.equipableType)
                        {
                            case EquipableType.ACCESSORY:
                                return TypeIds.Accessory;
                            case EquipableType.BODY:
                                return TypeIds.Body;
                            case EquipableType.LEGS:
                                return TypeIds.Legs;
                            case EquipableType.HEAD:
                                return TypeIds.Head;
                            case EquipableType.WEAPON:
                                if (equipableObj is MeleeWeaponScriptableObject)
                                    return TypeIds.Melee;
                                if (equipableObj is RangedWeaponScriptableObject)
                                    return TypeIds.Ranged;
                                return TypeIds.Error;
                        }
                    }
                break;
            }
            return TypeIds.Error;
        }
    }
}