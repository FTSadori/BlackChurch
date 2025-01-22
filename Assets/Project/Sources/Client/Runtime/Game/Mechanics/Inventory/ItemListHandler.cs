using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.ScriptableObjects;
using Client.Runtime.Game.ScriptableObjects.Lists;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace Client.Runtime.Game.Mechanics.Inventory
{
    [Serializable]
    public sealed class ItemListHandler
    {
        [SerializeField] private ItemListScriptableObject _itemListScriptableObject;

        public MaterialScriptableObject GetObjectById(string id)
        {
            var index = _itemListScriptableObject.ids.FindIndex(i => i == id);
            Assert.AreNotEqual(index, -1, "ItemListHandler.GetObjectById(): Can't find id " + id + " in ItemListScriptableObject");

            var item = _itemListScriptableObject.items[index];
            return item;
        }

        public SlotType GetNeededSlotTypeById(string id)
        {
            var itemObj = GetObjectById(id);

            if (itemObj.itemType == ItemType.EQUIPABLE)
            {
                if (itemObj is EquipableScriptableObject equipableObj)
                {
                    return equipableObj.equipableType switch
                    {
                        EquipableType.WEAPON => SlotType.WEAPON_ONLY,
                        EquipableType.ACCESSORY => SlotType.ACCESSORY_ONLY,
                        EquipableType.BODY => SlotType.BODY_ONLY,
                        EquipableType.HEAD => SlotType.HEAD_ONLY,
                        EquipableType.LEGS => SlotType.LEGS_ONLY,
                        _ => SlotType.EVERYTHING,
                    };
                }
                return SlotType.EVERYTHING;
            }
            else
            {
                return SlotType.EVERYTHING;
            }
        }
    }
}