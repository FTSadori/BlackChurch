using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Client.Runtime.Game.Environment.TrashPile.UI
{
    public sealed class TimeBarController : MonoBehaviour
    {
        [SerializeField] private GameObject _barFiller;
        [SerializeField] private float _barStartX = -0.936f;
        public Action OnComplete;

        public void StartTimer(float time) {            
            _barFiller.transform.localPosition = new Vector3(_barStartX, 0f);
    
            Sequence sequence = DOTween.Sequence();
            sequence.Insert(0f, _barFiller.transform.DOLocalMoveX(0f, time)).SetEase(Ease.Linear);

            sequence.OnComplete(() => {
                OnComplete?.Invoke();
            });
        }
    }
}