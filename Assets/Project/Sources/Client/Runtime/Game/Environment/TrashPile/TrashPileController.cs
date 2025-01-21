using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.Environment.TrashPile.UI;
using UnityEngine;

namespace Client.Runtime.Game.Environment.TrashPile
{
    public class TrashPileController : MonoBehaviour, ISelectable
    {
        [SerializeField] GameObject _selectionTexture;
        [SerializeField] GameObject _barObject;
        [SerializeField] TrashPileModel _trashPileModel;
        [SerializeField] TimeBarController _barController;

        private void Awake() {
            _barController.OnComplete += OnTimeUp;
        }

        public void Deselect()
        {
            _selectionTexture.SetActive(false);
        }

        public void Interact()
        {
            _barObject.SetActive(true);
            _barController.StartTimer(_trashPileModel._timeToFindItem);
        }

        public void Select()
        {
            _selectionTexture.SetActive(true);
        }

        private void OnTimeUp()
        {
            _barObject.SetActive(false);
            Debug.Log("Player has found something");
        }

        private void OnDestroy() {
            _barController.OnComplete -= OnTimeUp;
        }
    }
}