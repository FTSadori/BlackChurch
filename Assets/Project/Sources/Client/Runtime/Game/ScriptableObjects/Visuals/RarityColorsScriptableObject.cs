using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.ScriptableObjects;
using UnityEngine;

namespace Client.Runtime.Game.ScriptableObjects.Visuals
{
    [CreateAssetMenu(fileName = "RarityColors", menuName = "ScriptableObjects/Visual/RarityColors")]
    public class RarityColorsScriptableObject : ScriptableObject
    {
        public Color[] rarityColors = new Color[(int)Rarity.MAX_RARITY];
    }
}