using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework;
using TMPro;
using UnityEngine;

namespace Client.Runtime.Lobby.AwakeObjects
{
    public sealed class PlayerDataLoader : MonoBehaviour
    {
        [SerializeField] TMP_Text _nameText;

        void Start()
        {
            _nameText.text = StaticClientInfo._playerData.player;
        }
    }
}