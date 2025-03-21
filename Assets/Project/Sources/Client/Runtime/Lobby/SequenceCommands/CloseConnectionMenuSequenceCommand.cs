using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Unity.SceneCommands;
using DG.Tweening;
using UnityEngine;

namespace Client.Runtime.Lobby.SequenceCommands
{
    public sealed class CloseConnectionMenuSequenceCommand : SequenceCommand
    {
        [SerializeField] RectTransform _leftButton;
        [SerializeField] RectTransform _rightButton;
        [SerializeField] RectTransform _bottomButton;
        [SerializeField] RectTransform _backButton;
        [SerializeField] RectTransform _connectButton;
        [SerializeField] RectTransform _inputField;
        
        public override void Execute()
        {
            Sequence sequence = DOTween.Sequence();

            _leftButton.gameObject.SetActive(true);
            _rightButton.gameObject.SetActive(true);
            _bottomButton.gameObject.SetActive(true);

            _connectButton.gameObject.SetActive(false);
            _backButton.gameObject.SetActive(false);
            _inputField.gameObject.SetActive(false);

            sequence.Insert(0f, _leftButton.DOAnchorPosX(-127f, 0.3f));
            sequence.Insert(0f, _rightButton.DOAnchorPosX(127f, 0.3f));
            sequence.Insert(0f, _bottomButton.DOAnchorPosX(-22f, 0.3f));

            sequence.Insert(0f, _connectButton.DOAnchorPosX(360f, 0f));
            sequence.Insert(0f, _backButton.DOAnchorPosX(-360f, 0f));
            sequence.Insert(0f, _inputField.DOAnchorPosY(213.5f, 0f));

            sequence.OnComplete(() => { OnComplete?.Invoke(); });
        }
    }
}