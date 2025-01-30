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
        private Stack<ISelectable> _selectables = new();
        public ISelectable LastSelectable { get 
            {
                if (_selectables.Count == 0)
                    return null;
                return _selectables.Peek(); 
            } 
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.CompareTag("Selectable"))
            {
                LastSelectable?.Deselect();
                _selectables.Push(other.gameObject.GetComponent<ISelectable>());
                LastSelectable.Select();
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.gameObject.CompareTag("Selectable"))
            {
                LastSelectable.Deselect();
                _selectables.Pop();
                LastSelectable?.Select();
            }    
        }
    }
}