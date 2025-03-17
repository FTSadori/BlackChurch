using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            try{
                StaticClientInfo._playerData = new($"Player {RandomNumberGenerator.GetInt32(10, 100)}", "Nun", _testSprite);
                StaticClientInfo._socketClientHandler = new();
                var data = _inputField.text.Split(":");
                StaticClientInfo._socketClientHandler.Create();
                StaticClientInfo._socketClientHandler.Connect(data[0], int.Parse(data[1]));
                StaticClientInfo._socketClientHandler.Send($"{StaticClientInfo._playerData.player};{StaticClientInfo._playerData.character}");

                byte[] bytes = new byte[1024];
                StaticClientInfo._socketClientHandler.Receive(bytes);
                _inputField.text = Encoding.UTF8.GetString(bytes);
            }
            catch (Exception ex)
            {
                _inputField.text = ex.Message;
            }
        }
    }
}