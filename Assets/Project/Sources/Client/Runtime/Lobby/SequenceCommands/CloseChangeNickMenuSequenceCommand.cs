using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Unity.SceneCommands;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Runtime.Lobby.SequenceCommands
{
    public sealed class CloseChangeNickMenuSequenceCommand : SequenceCommand
    {
        [SerializeField] GameObject _wholeMenu;
        [SerializeField] Image _shadow;
        [SerializeField] RectTransform _backButton;
        [SerializeField] RectTransform _changeNickButton;
        [SerializeField] RectTransform _inputField;

        public override void Execute()
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Insert(0f, _shadow.DOFade(0f, 0.2f));

            sequence.Insert(0f, _changeNickButton.DOAnchorPosX(360f, 0.2f));
            sequence.Insert(0f, _backButton.DOAnchorPosX(-360f, 0.2f));
            sequence.Insert(0f, _inputField.DOAnchorPosY(213.5f, 0.2f));

            sequence.OnComplete(() => { _wholeMenu.SetActive(false); OnComplete?.Invoke(); });
        }
    }
}