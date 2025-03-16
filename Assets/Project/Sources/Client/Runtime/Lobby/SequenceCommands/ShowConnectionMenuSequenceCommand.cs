using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Unity.SceneCommands;
using DG.Tweening;
using UnityEngine;

namespace Client.Runtime.Lobby.SequenceCommands
{
    public sealed class ShowConnectionMenuSequenceCommand : SequenceCommand
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

            _leftButton.gameObject.SetActive(false);
            _rightButton.gameObject.SetActive(false);
            _bottomButton.gameObject.SetActive(false);

            _connectButton.gameObject.SetActive(true);
            _backButton.gameObject.SetActive(true);
            _inputField.gameObject.SetActive(true);

            sequence.Insert(0f, _leftButton.DOAnchorPosX(-427f, 0f));
            sequence.Insert(0f, _rightButton.DOAnchorPosX(427f, 0f));
            sequence.Insert(0f, _bottomButton.DOAnchorPosX(-322f, 0f));

            sequence.Insert(0f, _connectButton.DOAnchorPosX(160f, 0.3f));
            sequence.Insert(0f, _backButton.DOAnchorPosX(-160f, 0.3f));
            sequence.Insert(0f, _inputField.DOAnchorPosY(-13.5f, 0.3f));

            sequence.OnComplete(() => { OnComplete?.Invoke(); });
        }
    }
}