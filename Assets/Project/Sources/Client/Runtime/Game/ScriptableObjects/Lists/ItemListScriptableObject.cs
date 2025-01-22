using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Client.Runtime.Game.ScriptableObjects.Lists
{
    [CreateAssetMenu(fileName = "ItemList", menuName = "ScriptableObjects/Lists/ItemList")]
    public class ItemListScriptableObject : ScriptableObject
    {
        public List<string> ids = new();
        public List<MaterialScriptableObject> items = new();
    }
}