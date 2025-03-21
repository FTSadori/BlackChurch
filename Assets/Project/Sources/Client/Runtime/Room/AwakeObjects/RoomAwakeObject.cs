using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Client.Runtime.Framework;
using Client.Runtime.System.Messages;
using Server.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Runtime.Room.AwakeObjects
{
    public sealed class RoomAwakeObject : MonoBehaviour
    {
        [SerializeField] TMP_Text _roomTitle;
        [SerializeField] RoomController _roomController;
        [SerializeField] Sprite _nun;
        [SerializeField] Button _clientBackButton;
        [SerializeField] Button _serverBackButton;
        [SerializeField] Button _startButton;
        [SerializeField] Button _optionsButton;
        [SerializeField] ClientCommandMessagesLoader _clientCommandMessagesLoader;

        public static string _roomData = "";

        private void Awake() {
            if (StaticServerInfo._socketServerHandler.IsActive)
            {
                _roomTitle.text = $"Room's port: {StaticServerInfo._openedPort}";
                _roomController.AddPlayer(new (StaticClientInfo._playerData.player, "Nun", _nun));
                StaticServerInfo._socketServerHandler.StartListening();
                StaticServerInfo._socketServerHandler.OnConnect += AddNewPlayerFromServer;
                _clientBackButton.gameObject.SetActive(false);
            }
            else
            {
                StaticClientInfo._socketClientHandler.LoadCommands(_clientCommandMessagesLoader);
                StaticClientInfo._socketClientHandler.StartReceivingThread();
                _serverBackButton.gameObject.SetActive(false);
                _startButton.gameObject.SetActive(false);
                _optionsButton.gameObject.SetActive(false);
                LoadFromLine(_roomData);
            }
        }

        public void LoadFromLine(string line)
        {
            _roomController.ClearPlayerData();

            line = line.Trim(new char[] { '\0', ' ' });
            var parts = line.Split(';');
            _roomTitle.text = $"Room's port: {parts[0]}";

            for (int i = 1; i < parts.Length; i += 2)
            {
                AddNewPlayerFromServer(new (parts[i], parts[i + 1]));
            }
        }

        private void AddNewPlayerFromServer(ShortPlayerData shortPlayerData)
        {
            _roomController.AddPlayer(new (shortPlayerData.name, shortPlayerData.characterId, _nun));
        }

        void OnDestroy()
        {
            if (StaticServerInfo._socketServerHandler.IsActive)
            {
                StaticServerInfo._socketServerHandler.OnConnect -= AddNewPlayerFromServer;
            }
        }
    }
}