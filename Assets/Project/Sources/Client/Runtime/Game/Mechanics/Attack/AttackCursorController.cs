using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.Player;
using UnityEngine;
using UnityEngine.Assertions;

namespace Client.Runtime.Game.Mechanics.Attack
{
    public sealed class AttackCursorController : MonoBehaviour
    {
        [SerializeField] private GameObject _centerObject;
        [SerializeField] private GameObject _cursorObject;
        [SerializeField] private float _distanceFromPlayer;

        private Vector2 _cursorDirection;
        private float _cursorAngle;

        public Vector3 CursorDirection => _cursorDirection;
        public float CursorAngle => _cursorAngle;

        private void Awake()
        {
        }

        private void Update()
        {
            UpdateCursorPosition();
        }

        private void UpdateCursorPosition()
        {
            var playerPos = Camera.main.WorldToScreenPoint(_centerObject.transform.position);

            _cursorDirection = new Vector2(Input.mousePosition.x - playerPos.x, Input.mousePosition.y - playerPos.y).normalized;

            _cursorAngle = Mathf.Asin(_cursorDirection.y) * 180f / Mathf.PI;
            if (Mathf.Sign(_cursorDirection.x) == -1f)
                _cursorAngle = 180f - _cursorAngle;

            var pos = new Vector2(_centerObject.transform.position.x, _centerObject.transform.position.y) + _distanceFromPlayer * _cursorDirection;
            _cursorObject.transform.position = pos;
        }
    }
}