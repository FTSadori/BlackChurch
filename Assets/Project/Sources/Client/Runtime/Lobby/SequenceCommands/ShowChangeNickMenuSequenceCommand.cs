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
    public sealed class ShowChangeNickMenuSequenceCommand : SequenceCommand
    {
        [SerializeField] GameObject _wholeMenu;
        [SerializeField] Image _shadow;
        [SerializeField] RectTransform _backButton;
        [SerializeField] RectTransform _changeNickButton;
        [SerializeField] RectTransform _inputField;

        public override void Execute()
        {
            Sequence sequence = DOTween.Sequence();

            _wholeMenu.SetActive(true);
            sequence.Insert(0f, _shadow.DOFade(0.42f, 0.3f));

            sequence.Insert(0f, _changeNickButton.DOAnchorPosX(160f, 0.3f));
            sequence.Insert(0f, _backButton.DOAnchorPosX(-160f, 0.3f));
            sequence.Insert(0f, _inputField.DOAnchorPosY(-13.5f, 0.3f));

            sequence.OnComplete(() => { OnComplete?.Invoke(); });
        }
    }
}