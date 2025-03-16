using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Runtime.Room.AwakeObjects
{
    public sealed class RoomSlotController : MonoBehaviour
    {
        [SerializeField] TMP_Text _playerText;
        [SerializeField] TMP_Text _characterText;
        [SerializeField] Image _characterImage;

        public void Unset()
        {
            _playerText.text = "";
            _characterText.text = "";
            _characterImage.gameObject.SetActive(false);
        }

        public void Set(PlayerData playerData)
        {
            _playerText.text = playerData.player;
            _characterText.text = playerData.character;
            _characterImage.gameObject.SetActive(true);
            _characterImage.sprite = playerData.image;
        }
    }
}