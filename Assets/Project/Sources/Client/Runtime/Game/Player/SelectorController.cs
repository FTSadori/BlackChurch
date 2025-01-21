using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.Environment;
using UnityEngine;

namespace Client.Runtime.Game.Player
{
    public sealed class SelectorController : MonoBehaviour
    {
        private GameObject _lastGameObject = null;
        public ISelectable LastSelectable { get 
            {
                if (_lastGameObject == null)
                    return null;
                return _lastGameObject.GetComponent<ISelectable>(); 
            } 
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.CompareTag("Selectable"))
            {
                if (_lastGameObject != null) LastSelectable.Deselect();
                _lastGameObject = other.gameObject;
                LastSelectable.Select();
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.gameObject.CompareTag("Selectable"))
            {
                if (other.gameObject.Equals(_lastGameObject))
                {
                    LastSelectable.Deselect();
                    _lastGameObject = null;
                }
            }    
        }
    }
}