using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.Mechanics.Inventory.Data;
using Client.Runtime.Game.ScriptableObjects.Visuals;
using Client.Runtime.Game.UI;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField] private Button _craftButton;
        
        private string currentMaterial0 = "";
        private string currentMaterial1 = "";

        public string GetCurrentMaterial(int num) => (num == 0) ? currentMaterial0 : currentMaterial1;

        public void Set(ItemData itemData)
        {
            currentMaterial0 = itemData.leftMaterialId;
            currentMaterial1 = itemData.rightMaterialId;

            _result.Set(itemData.id, 1);
            _leftMaterial.Set(itemData.leftMaterialId, 1);
            _rightMaterial.Set(itemData.rightMaterialId, 1);

            _craftButton.interactable = itemData.canBeCraftedNow;
        }
    }
}