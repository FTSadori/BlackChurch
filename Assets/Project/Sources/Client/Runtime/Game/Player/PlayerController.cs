using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Client.Runtime.Game.Player
{
    public sealed class PlayerController : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private GroundDetectorModel _groundDetectorModel;
        [SerializeField] private GroundDetectorView _groundDetectorView;
        [SerializeField] private SelectorController _selectorController;

        [Header("Constants")]
        [SerializeField] private float _maxVerticalFallingSpeed = 1f;
        [SerializeField] private float _fallGravityMultiplier = 1.5f;
        [SerializeField] private float _ungroundedJumpTime = 0.2f;
        [SerializeField] private float _jumpCommandTime = 0.2f;

        private float _ungroundedJumpTimer = -1f;
        private float _jumpCommandTimer = -1f;
        
        private void Awake() {
            _groundDetectorView.OnEnterGround += OnEnterGround;
            _groundDetectorView.OnExitGround += OnExitGround;
        }

        private void OnDestroy() {
            _groundDetectorView.OnEnterGround -= OnEnterGround;
            _groundDetectorView.OnExitGround -= OnExitGround;
        }

        private void Update() {
            _ungroundedJumpTimer -= Time.deltaTime;
            _jumpCommandTimer -= Time.deltaTime;

            CheckJump();
            CheckFalling();
            CheckInteraction();
        }

        private void FixedUpdate() {
            CheckHorizontalMove();    
        }

        private void CheckInteraction() {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _selectorController.LastSelectable?.Interact();
            }
        }

        private void CheckJump() {
            if ((Input.GetKeyDown(KeyCode.Space) || _jumpCommandTimer > 0f) && 
                ((_groundDetectorModel.IsGrounded && Mathf.Abs(_playerView.Rigidbody.velocity.y) < 0.001f) || _ungroundedJumpTimer > 0f))
            {
                // revert faster falling speed
                _playerView.Rigidbody.gravityScale = _playerModel.BaseGravity;

                // add vertical speed
                var oldX = _playerView.Rigidbody.velocity.x;

                // if spacebar is already up, then jump is halved
                var newY = _playerModel.JumpVelocity * (Input.GetKey(KeyCode.Space) ? 1f : 0.5f);
                _playerView.Rigidbody.velocity = new Vector2(oldX, newY);

                // unset the timers
                _ungroundedJumpTimer = -1f;
                _jumpCommandTimer = -1f;
            }
            // if player not grounded but has pressed jump button
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                _jumpCommandTimer = _jumpCommandTime;
            }
            // if player releases jump button, then velocity halves
            else if (Input.GetKeyUp(KeyCode.Space) && _playerView.Rigidbody.velocity.y > 0)
            {
                var oldX = _playerView.Rigidbody.velocity.x;
                _playerView.Rigidbody.velocity = new Vector2(oldX, _playerView.Rigidbody.velocity.y / 2f);
            }
        }

        private void CheckFalling() {
            // if player is falling...
            if (_playerView.Rigidbody.velocity.y < 0)
            {
                // ...make gravity stronger
                _playerView.Rigidbody.gravityScale = _playerModel.BaseGravity * _fallGravityMultiplier;

                if (_playerView.Rigidbody.velocity.y < -_maxVerticalFallingSpeed)
                {
                    var oldX = _playerView.Rigidbody.velocity.x;
                    _playerView.Rigidbody.velocity = new Vector2(oldX, -_maxVerticalFallingSpeed);
                }
            }
        }

        private void CheckHorizontalMove() {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var delta = _playerModel.MaxSpeed * Time.fixedDeltaTime;

            _playerView.Rigidbody.position += new Vector2(horizontal * delta, 0f);
        }

        private void OnEnterGround()
        {
            _playerView.Rigidbody.gravityScale = _playerModel.BaseGravity;
        }

        private void OnExitGround()
        {
            // set timer for extra jump
            _ungroundedJumpTimer = _ungroundedJumpTime;
        }
    }
}