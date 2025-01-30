using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.Player;
using UnityEngine;

namespace Client.Runtime.Game.Environment
{
    public sealed class OneWayPlatformController : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _collider;
        [SerializeField] private PlayerController _playerController;

        private void OnCollisionStay2D(Collision2D other) {
            if (other.gameObject.CompareTag("Character") && _playerController.CanGoDown)
            {
                _collider.isTrigger = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.gameObject.CompareTag("Character"))
            {
                _collider.isTrigger = false;
            }
        }
    }
}