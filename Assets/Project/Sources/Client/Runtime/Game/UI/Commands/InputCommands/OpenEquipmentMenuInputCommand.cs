using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackChurch.Assets.Project.Sources.Client.Runtime.Game.UI.Menu;
using Client.Runtime.Framework.Unity;
using Client.Runtime.Game.Mechanics.Inventory;
using Client.Runtime.Game.UI.Menu;
using UnityEngine;

namespace Client.Runtime.Game.UI.Commands.InputCommands
{
    public sealed class OpenEquipmentMenuInputCommand : MonoCommand
    {
        [SerializeField] OpenMenuCommand _openEquipmentMenu;
        [SerializeField] MenuController _menuController;
        [SerializeField] EquipmentMenuInputController _equipmentMenuInputController;
        [SerializeField] private ToolbarController _toolbarController;

        public override void Execute()
        {
            _openEquipmentMenu.Execute();
            _menuController.Push(_equipmentMenuInputController);
            _toolbarController.SetHelpNumberVisibility(false);
        }
    }
}