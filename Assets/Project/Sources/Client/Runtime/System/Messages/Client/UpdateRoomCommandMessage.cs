using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework;
using Client.Runtime.Room.AwakeObjects;
using UnityEngine;

namespace Client.Runtime.System.Messages.Client
{
    public sealed class UpdateRoomCommandMessage : AbstractCommandMessage
    {
        [SerializeField] RoomAwakeObject _roomAwakeObject;

        public override string CommandName => "updateRoom";

        public override void Execute(string line)
        {
            Debug.Log($"Room data: {line}");
            _roomAwakeObject.LoadFromLine(line);
        }
    }
}