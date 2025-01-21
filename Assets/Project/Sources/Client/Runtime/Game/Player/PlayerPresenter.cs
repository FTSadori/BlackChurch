using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Presenter;
using UnityEngine;

namespace Client.Runtime.Game.Player
{
    public sealed class PlayerPresenter : MonoPresenter
    {
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private PlayerView _playerView;

        private void Awake() {
            Enable();
        }

        private void OnDestroy() {
            Disable();
        }

        public override void Enable()
        {
        }

        public override void Disable()
        {
        }
    }
}