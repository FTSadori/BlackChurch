using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.ScriptableObjects.Visuals;
using Client.Runtime.Game.UI;
using UnityEngine;

namespace Client.Runtime.Game.Mechanics.Inventory
{
    public sealed class CraftMenuController : MonoBehaviour
    {
        [SerializeField] private ItemListHandler _itemListHandler;
        [SerializeField] private RarityColorsScriptableObject _rarityColorsScriptableObject;
        [SerializeField] private ToolbarModel _toolbarModel;

        [Header("Components")]
        [SerializeField] private ItemSlotController _leftMaterial;
        [SerializeField] private ItemSlotController _rightMaterial;
        [SerializeField] private ItemSlotController _result;
         

        public void Set(string id)
        {
            var itemObj = _itemListHandler.GetObjectById(id);

            _result.Set(id, 1);
            _leftMaterial.Set(itemObj.craftsFromId1, 1);
            _rightMaterial.Set(itemObj.craftsFromId2, 1);
        }
    }
}