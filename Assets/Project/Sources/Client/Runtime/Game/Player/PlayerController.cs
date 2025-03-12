using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.Environment;
using Client.Runtime.Game.Player.Commands;
using Client.Runtime.Game.ScriptableObjects.Items;
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
        [SerializeField] private Animator _animator;
        [SerializeField] private Animator _knifeAnimator;
        [SerializeField] private Animator _noHandsAnimator;
        [SerializeField] private PlayerAttackCommand _playerAttackCommand;

        [Header("Constants")]
        [SerializeField] private float _maxVerticalFallingSpeed = 1f;
        [SerializeField] private float _fallGravityMultiplier = 1.5f;
        [SerializeField] private float _ungroundedJumpTime = 0.2f;
        [SerializeField] private float _jumpCommandTime = 0.2f;

        public bool AutoChangeDirection = true;
        private int _lastDirection = 1;
        public int LastDirection { 
            get => _lastDirection; 
            set {
                _lastDirection = value;
                _playerView.SpriteRenderer.flipX = _lastDirection == -1;
                _noHandsAnimator.gameObject.GetComponent<SpriteRenderer>().flipX = _lastDirection == -1;
                }
        }
        public bool CanMove = true;
        public bool CompletelyPaused = false;

        public bool CanGoDown => CanMove && !CompletelyPaused && Input.GetKey(KeyCode.S);

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
            if (CompletelyPaused) return;

            _ungroundedJumpTimer -= Time.deltaTime;
            _jumpCommandTimer -= Time.deltaTime;

            if (CanMove)
            {
                CheckJump();
                CheckInteraction();
                CheckAttack();
            }
            CheckFalling();
        }

        private void CheckAttack()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _playerAttackCommand.Execute();
            }
        }

        private void FixedUpdate() {
            if (CanMove && !CompletelyPaused)
            {
                CheckHorizontalMove();
                _animator.SetBool("Moves", (int)Input.GetAxisRaw("Horizontal") != 0);     
                _knifeAnimator.SetBool("Moves", (int)Input.GetAxisRaw("Horizontal") != 0); 
                _noHandsAnimator.SetBool("Moves", (int)Input.GetAxisRaw("Horizontal") != 0);
            }
            else
            {
                _animator.SetBool("Moves", false);
                _knifeAnimator.SetBool("Moves", false);
                _noHandsAnimator.SetBool("Moves", false);
            }
        }

        private void CheckInteraction() {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _selectorController.LastSelectable?.Interact(InteractType.First);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                _selectorController.LastSelectable?.Interact(InteractType.Second);
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

            _animator.SetBool("Jumps", _playerView.Rigidbody.velocity.y < -1f);
            _knifeAnimator.SetBool("Jumps", _playerView.Rigidbody.velocity.y < -1f);
            _noHandsAnimator.SetBool("Jumps", _playerView.Rigidbody.velocity.y < -1f);
        }

        private void CheckHorizontalMove() {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var delta = _playerModel.MaxSpeed * Time.fixedDeltaTime;
            if (AutoChangeDirection && (int)horizontal != 0)
                LastDirection = (int)horizontal;
        

            _playerView.Rigidbody.position += new Vector2(horizontal * delta, 0f);
        }

        private void OnEnterGround()
        {
            _animator.SetBool("Jumps", false);
            _knifeAnimator.SetBool("Jumps", false);
            _noHandsAnimator.SetBool("Jumps", false);
            _playerView.Rigidbody.gravityScale = _playerModel.BaseGravity;
        }

        private void OnExitGround()
        {
            // set timer for extra jump
            _ungroundedJumpTimer = _ungroundedJumpTime;
        }
    }
}