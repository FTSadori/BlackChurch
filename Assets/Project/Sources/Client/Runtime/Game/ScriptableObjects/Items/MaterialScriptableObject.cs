using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Client.Runtime.Game.ScriptableObjects
{
    public enum ItemType
    {
        CONSUMABLE,
        MATERIAL,
        EQUIPABLE,
    }

    public enum Rarity
    {
        COMMON,
        UNCOMMON,
        RARE,
        EPIC,
        LEGENDARY,
        DEMONLIKE,
        GODLIKE,
        MAX_RARITY,
    }

    [CreateAssetMenu(fileName = "Material", menuName = "ScriptableObjects/Items/Material")]
    public class MaterialScriptableObject : ScriptableObject
    {
        public string id;
        public Sprite sprite;
        public Rarity rarity;
        public uint quantity;
        public uint stack;
        public ItemType itemType;
        public string craftsFromId1;
        public string craftsFromId2;
    }
}