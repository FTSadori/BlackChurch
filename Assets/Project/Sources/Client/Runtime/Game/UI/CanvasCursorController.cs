using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Client.Runtime.Game.UI
{
    public class CanvasCursorController : MonoBehaviour
    {
        [SerializeField] RectTransform _cursorRectTransform;
        [SerializeField] RectTransform _canvasRectTransform;

        private void Awake() {
            //Cursor.visible = false;
        }

        private void Update() {
            var mousePos = Input.mousePosition;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRectTransform, mousePos, null, out Vector2 v);
            _cursorRectTransform.anchoredPosition = v;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Slots"))
            {
                other.gameObject.GetComponent<ItemSlotController>().Select();
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Slots"))
            {
                other.gameObject.GetComponent<ItemSlotController>().Deselect();                
            }
        }
    }
}