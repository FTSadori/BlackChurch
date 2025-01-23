using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Runtime.Game.Mechanics.Inventory
{
    public enum SlotType
    {
        EVERYTHING,
        WEAPON_ONLY,
        HEAD_ONLY,
        BODY_ONLY,
        LEGS_ONLY,
        ACCESSORY_ONLY,
    }

    public sealed class InventoryDataRecord
    {
        public string id;
        public int quantity;
        public SlotType type;

        public InventoryDataRecord(string _id, int _quantity, SlotType _type)
        {
            id = _id;
            quantity = _quantity;
            type = _type;
        }

        public bool CanFit(SlotType slotType)
        {
            return (type == SlotType.EVERYTHING) || (type == slotType);
        }
    }
}