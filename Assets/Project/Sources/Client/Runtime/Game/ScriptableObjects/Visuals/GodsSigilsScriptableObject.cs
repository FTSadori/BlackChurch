using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sources.Client.Runtime.Game.Mechanics;
using UnityEngine;

namespace Client.Runtime.Game.ScriptableObjects.Visuals
{
    [CreateAssetMenu(fileName = "GodsSigils", menuName = "ScriptableObjects/Visual/GodsSigils")]
    public sealed class GodsSigilsScriptableObject : ScriptableObject
    {
        public Color[] sigilColors = new Color[(int)GodType.MAX_TYPE];
        public Sprite[] sigilSprites = new Sprite[(int)GodType.MAX_TYPE];
    }
}