using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.Environment;
using Client.Runtime.Game.UI.AnimationControllers;
using UnityEngine;

namespace Client.Runtime.Game.Player
{
    public sealed class SelectorController : MonoBehaviour
    {
        [SerializeField] HelpBoxController _helpBoxController;

        private List<GameObject> _selectables = new();
        public ISelectable LastSelectable { get 
            {
                if (_selectables.Count == 0)
                    return null;
                return _selectables.Last().GetComponent<ISelectable>(); 
            } 
        }

        public HelpBoxType _currentState = HelpBoxType.NONE;

        private void OnTriggerStay2D(Collider2D other) {
            if (other.gameObject.CompareTag("Selectable"))
            {
                if (_currentState != LastSelectable.HelpBoxType)
                {
                    _helpBoxController.Raise(LastSelectable.HelpBoxType);
                    _currentState = LastSelectable.HelpBoxType;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.CompareTag("Selectable"))
            {
                LastSelectable?.Deselect();
                _selectables.Add(other.gameObject);
                LastSelectable.Select();
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.gameObject.CompareTag("Selectable"))
            {
                LastSelectable.Deselect();
                _selectables.RemoveAll(i => i.GetInstanceID() == other.gameObject.GetInstanceID());
                LastSelectable?.Select();
                if (_selectables.Count == 0)
                {
                    _currentState = HelpBoxType.NONE;
                    _helpBoxController.Remove();
                }
            }    
        }

        private void OnDestroy() {
            _selectables.Clear();
        }
    }
}