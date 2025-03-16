using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.Player;
using UnityEngine;
using TMPro;
using Client.Runtime.Game.ScriptableObjects;
using Sources.Client.Runtime.Game.Mechanics;

namespace Client.Runtime.Game.Mechanics.Inventory.MenuControllers
{
    public sealed class StatMenuController : MonoBehaviour
    {
        [SerializeField] PlayerModel _playerModel;
        [SerializeField] ItemListHandler _itemListHandler;

        [SerializeField] TMP_Text[] _godBuffsText = new TMP_Text[(int)GodType.MAX_TYPE];
        [SerializeField] TMP_Text _nameText;
        [SerializeField] TMP_Text _statsText;

        [SerializeField] EquipmentModel _equipmentModel;

        private void Awake() {
            PlayerModel.MainPlayerGodBuffs.OnUpdate += UpdateStats;
            // ?? _playerModel.currentStats.OnStatsUpdate += UpdateStats;

            _equipmentModel.InventoryData.OnUpdateInventory += UpdateStats;

            _playerModel.OnHpUpdate += UpdateBigStatsLabel;

            UpdateStats();
        }

        private void UpdateStats() {
            _playerModel.currentStats = StatsCalculator.Recalculate(_playerModel.scriptableObject.baseStats, _equipmentModel.InventoryData, _itemListHandler, PlayerModel.MainPlayerGodBuffs);
            
            for (int i = 0; i < (int)GodType.MAX_TYPE - 1; ++i)
            {
                _godBuffsText[i].text = PlayerModel.MainPlayerGodBuffs.GetBuff((GodType)i).ToString("0.0");
            }
            _godBuffsText.Last().text = "Total: " + PlayerModel.MainPlayerGodBuffs.GetBuff(GodType.TIME).ToString("0.0");

            _nameText.text = _playerModel.scriptableObject.characterName;
            UpdateBigStatsLabel();
        }

        private void UpdateBigStatsLabel() {
            var stats = _playerModel.currentStats;
            _statsText.text = $"HP: {_playerModel.HP}/{stats.maxHp} (+{stats.regenerationRate:0.0}/s)\nAtk: {stats.baseAttack} (+{stats.pureAttack})\nDef: {stats.baseDefence} (+{stats.pureDefence})\nSpeed: {stats.speedBuff * 100f:0.0}%";
        }

        private void OnDestroy() {
            PlayerModel.MainPlayerGodBuffs.OnUpdate -= UpdateStats;
            // ?? _playerModel.currentStats.OnStatsUpdate -= UpdateStats;

            _equipmentModel.InventoryData.OnUpdateInventory -= UpdateStats;

            _playerModel.OnHpUpdate -= UpdateBigStatsLabel;          
        }
    }
}