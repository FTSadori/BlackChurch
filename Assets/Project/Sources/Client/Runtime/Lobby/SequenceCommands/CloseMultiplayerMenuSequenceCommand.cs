using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Unity.SceneCommands;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BlackChurch.Assets.Project.Sources.Client.Runtime.Lobby.SequenceCommands
{
    public sealed class CloseMultiplayerMenuSequenceCommand : SequenceCommand
    {
        [SerializeField] GameObject _wholeMenu;
        [SerializeField] Image _shadow;
        [SerializeField] RectTransform _leftButton;
        [SerializeField] RectTransform _rightButton;
        [SerializeField] RectTransform _buttonButton;

        public override void Execute()
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Insert(0f, _shadow.DOFade(0f, 0.3f));
            sequence.Insert(0f, _leftButton.DOAnchorPosX(-427f, 0.3f));
            sequence.Insert(0f, _rightButton.DOAnchorPosX(427f, 0.3f));
            sequence.Insert(0f, _buttonButton.DOAnchorPosX(-322f, 0.3f));
            
            sequence.OnComplete(() => { _wholeMenu.SetActive(false); OnComplete?.Invoke(); });
        }
    }
}