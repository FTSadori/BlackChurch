using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Client.Runtime.Game.ScriptableObjects.Piles
{
    [CreateAssetMenu(fileName = "TrashPile", menuName = "ScriptableObjects/TrashPile")]
    public sealed class TrashPileScriptableObject : ScriptableObject
    {
        public float timeToFindItem;
        public List<string> itemIds;
    }
}