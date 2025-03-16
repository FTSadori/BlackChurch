using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Unity;
using Client.Runtime.Lobby.SceneCommands;
using UnityEngine;
using Server.Framework;
using System.Security.Cryptography;

namespace Client.Runtime.Lobby.ButtonCommands
{
    public sealed class CreateRoomButtonCommand : ButtonCommand
    {
        [SerializeField] OpenRoomSceneCommand _openRoomSceneCommand;

        public override void Execute()
        {
            Debug.Log("Create room: Starts loading");
            StaticServerInfo._socketServerHandler = new();
            StaticServerInfo._openedPort = RandomNumberGenerator.GetInt32(10000, 60000);
            StaticServerInfo._socketServerHandler.EstablishEndpoint(StaticServerInfo._openedPort);
            Debug.Log("Create room: Ends loading");

            _openRoomSceneCommand.Execute();
        }
    }
}