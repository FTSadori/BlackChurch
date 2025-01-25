using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.ScriptableObjects;

namespace Client.Runtime.Game.Mechanics.Inventory.Data
{
    public sealed class ItemData
    {
        public string id;
        public string nameId;
        public string descriptionId;
        public string typeId;
        public string rarityId;
        public bool craftButtonActive;
        public bool useButtonActive;
        public bool discardButtonActive;
        public string leftMaterialId;
        public string rightMaterialId;
        public bool canBeCraftedNow;
    }
}