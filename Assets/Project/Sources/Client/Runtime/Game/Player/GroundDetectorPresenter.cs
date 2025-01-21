using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Presenter;
using UnityEngine;

namespace Client.Runtime.Game.Player
{
    public sealed class GroundDetectorPresenter : MonoPresenter
    {
        [SerializeField] GroundDetectorModel _groundDetectorModel;
        [SerializeField] GroundDetectorView _groundDetectorView;

        private void Awake() {
            Enable();
        }

        private void OnDestroy() {
            Disable();
        }

        public override void Enable()
        {
            _groundDetectorView.OnEnterGround += OnEnterGround;
            _groundDetectorView.OnExitGround += OnExitGround;
        }

        public override void Disable()
        {
            _groundDetectorView.OnEnterGround -= OnEnterGround;
            _groundDetectorView.OnExitGround -= OnExitGround;
        }   

        public void OnEnterGround()
        {
            _groundDetectorModel.IsGrounded = true;
        }

        public void OnExitGround()
        {
            _groundDetectorModel.IsGrounded = false;
        }
    }
}