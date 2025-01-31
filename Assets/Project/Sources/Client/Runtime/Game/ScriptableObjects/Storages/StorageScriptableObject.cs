using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Framework.Base;
using UnityEngine;

namespace Client.Runtime.Game.ScriptableObjects.Storages
{
    [CreateAssetMenu(fileName = "Storage", menuName = "ScriptableObjects/Storages")]
    public class StorageScriptableObject : ScriptableObject
    {
        public bool canPlayerPutItemsIn;
        public List<Pair<string, int>> items;
    }
}