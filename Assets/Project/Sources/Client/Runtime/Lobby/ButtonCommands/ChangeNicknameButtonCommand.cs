using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework;
using Client.Runtime.Framework.Unity;
using TMPro;
using UnityEngine;

namespace Client.Runtime.Lobby.ButtonCommands
{
    public sealed class ChangeNicknameButtonCommand : ButtonCommand
    {
        [SerializeField] TMP_InputField _inputField;
        [SerializeField] TMP_Text _nickField;

        public override void Execute()
        {
            if (_inputField.text.Contains(";") || _inputField.text.Contains("#") || _inputField.text.Length == 0)
            {
                _inputField.text = "That's a shitty name, mate";
                return;
            }
            StaticClientInfo._playerData.player = _inputField.text;
            _nickField.text = _inputField.text;
        }
    }
}