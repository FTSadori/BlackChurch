using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Client.Runtime.Room.AwakeObjects
{
    public sealed class PlayerData 
    {
        public string player;
        public string character;
        public Sprite image;

        public PlayerData(string player, string character, Sprite image)
        {
            this.player = player;
            this.character = character;
            this.image = image;
        }
    }

    public sealed class RoomController : MonoBehaviour
    {
        [SerializeField] List<RoomSlotController> _slots = new();

        public int SlotIndex { get; private set; } = 0;
        public List<PlayerData> playerDatas = new();

        public void AddPlayer(PlayerData playerData)
        {
            if (SlotIndex >= _slots.Count)
            {
                return;
            }
            playerDatas.Add(playerData);

            UpdateSlots();
        }

        public void RemovePlayer(PlayerData playerData)
        {
            playerDatas.RemoveAll(p => p.player == playerData.player);

            UpdateSlots();
        }

        public void UpdateSlots()
        {
            foreach (RoomSlotController slot in _slots)
            {
                slot.Unset();
            }
            for (int i = 0; i < playerDatas.Count; ++i)
            {
                _slots[i].Set(playerDatas[i]);
            }
        }
    }
}