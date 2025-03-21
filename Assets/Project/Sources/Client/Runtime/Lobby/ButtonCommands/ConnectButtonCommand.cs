using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Client.Runtime.Framework;
using Client.Runtime.Framework.Unity;
using Client.Runtime.Lobby.SceneCommands;
using Client.Runtime.Room.AwakeObjects;
using Client.Runtime.System.Messages;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Runtime.Lobby.ButtonCommands
{
    public sealed class ConnectButtonCommand : ButtonCommand
    {
        [SerializeField] TMP_InputField _inputField;
        [SerializeField] Sprite _testSprite;
        [SerializeField] OpenRoomSceneCommand _openRoomSceneCommand;

        public override void Execute()
        {
            try{
                StaticClientInfo._playerData.character = "Nun";
                StaticClientInfo._playerData.image = _testSprite;
                StaticClientInfo._socketClientHandler = new();
                var data = _inputField.text.Split(":");
                StaticClientInfo._socketClientHandler.Create();
                StaticClientInfo._socketClientHandler.Connect(data[0], int.Parse(data[1]));
                StaticClientInfo._socketClientHandler.Send($"{StaticClientInfo._playerData.player};{StaticClientInfo._playerData.character}");

                byte[] bytes = new byte[1024];
                StaticClientInfo._socketClientHandler.Receive(bytes);
                RoomAwakeObject._roomData = Encoding.UTF8.GetString(bytes).Trim(new char[] { ' ', '\0' });

                _openRoomSceneCommand.Execute();
            }
            catch (Exception ex)
            {
                _inputField.text = ex.Message;
            }
        }
    }
}