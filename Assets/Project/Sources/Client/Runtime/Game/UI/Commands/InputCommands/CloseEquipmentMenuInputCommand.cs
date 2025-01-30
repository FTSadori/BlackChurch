using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Unity;
using Client.Runtime.Game.Mechanics.Inventory;
using Client.Runtime.Game.UI.Menu;
using UnityEngine;

namespace Client.Runtime.Game.UI.Commands.InputCommands
{
    public sealed class CloseEquipmentMenuInputCommand : MonoCommand
    {
        [SerializeField] MenuController _menuController;
        [SerializeField] CloseMenuCommand _closeMenuCommand;
        [SerializeField] ToolbarController _toolbarController;        

        public override void Execute()
        {
            _closeMenuCommand.Execute();
            _menuController.Pop();
            _toolbarController.SetHelpNumberVisibility(true);
        }
    }
}