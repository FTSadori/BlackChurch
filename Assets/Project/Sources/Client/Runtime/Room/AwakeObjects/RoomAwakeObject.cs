using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Server.Framework;
using TMPro;
using UnityEngine;

namespace Client.Runtime.Room.AwakeObjects
{
    public sealed class RoomAwakeObject : MonoBehaviour
    {
        [SerializeField] TMP_Text _roomTitle;
        [SerializeField] RoomController _roomController;
        [SerializeField] Sprite _nun;

        private void Awake() {
            if (StaticServerInfo._socketServerHandler.IsActive)
            {
                _roomTitle.text = $"Room's port: {StaticServerInfo._openedPort}";
                _roomController.AddPlayer(new ($"Server {RandomNumberGenerator.GetInt32(10, 100)}", "Nun", _nun));
                StaticServerInfo._socketServerHandler.StartListening();
                StaticServerInfo._socketServerHandler.OnConnect += AddNewPlayerFromServer;
            }
            else
            {
            }
        }

        private void AddNewPlayerFromServer()
        {
            _roomController.AddPlayer(new ($"Client {RandomNumberGenerator.GetInt32(10, 100)}", "Nun", _nun));
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