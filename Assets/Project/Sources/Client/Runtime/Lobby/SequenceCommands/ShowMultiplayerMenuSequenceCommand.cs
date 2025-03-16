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
    public class ShowMultiplayerMenuSequenceCommand : SequenceCommand
    {
        [SerializeField] GameObject _wholeMenu;
        [SerializeField] Image _shadow;
        [SerializeField] RectTransform _leftButton;
        [SerializeField] RectTransform _rightButton;
        [SerializeField] RectTransform _buttonButton;

        public override void Execute()
        {
            Sequence sequence = DOTween.Sequence();
            _wholeMenu.SetActive(true);

            sequence.Insert(0f, _shadow.DOFade(0.42f, 0.3f));
            sequence.Insert(0f, _leftButton.DOAnchorPosX(-127f, 0.3f));
            sequence.Insert(0f, _rightButton.DOAnchorPosX(127f, 0.3f));
            sequence.Insert(0f, _buttonButton.DOAnchorPosX(-22f, 0.3f));
            
            sequence.OnComplete(() => { OnComplete?.Invoke(); });
        }
    }
}