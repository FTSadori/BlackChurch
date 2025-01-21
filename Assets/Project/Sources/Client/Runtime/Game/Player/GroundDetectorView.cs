using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Client.Runtime.Game.Player
{
    public sealed class GroundDetectorView : MonoBehaviour
    {
        public Action OnEnterGround;
        public Action OnExitGround;

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.CompareTag("Ground"))
            {
                OnEnterGround?.Invoke();
            }   
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.gameObject.CompareTag("Ground"))
            {
                OnExitGround?.Invoke();
            }
        }
    }
}