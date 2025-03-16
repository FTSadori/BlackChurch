using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Client.Runtime.Framework;
using Client.Runtime.Framework.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Runtime.Lobby.ButtonCommands
{
    public sealed class ConnectButtonCommand : ButtonCommand
    {
        [SerializeField] TMP_InputField _inputField;
        [SerializeField] Sprite _testSprite;

        public override void Execute()
        {
//            try{
                StaticClientInfo._playerData = new($"Player {RandomNumberGenerator.GetInt32(10, 100)}", "Nun", _testSprite);
                StaticClientInfo._socketClientHandler = new();
                var data = _inputField.text.Split(":");
                StaticClientInfo._socketClientHandler.Create();
                StaticClientInfo._socketClientHandler.Connect(data[0], int.Parse(data[1]));
//            }
//            catch (Exception ex)
//            {
//                _inputField.text = ex.Message;
//            }
        }
    }
}