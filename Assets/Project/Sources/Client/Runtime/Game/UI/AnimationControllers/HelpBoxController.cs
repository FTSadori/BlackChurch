using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Client.Runtime.Game.UI.AnimationControllers
{
    public enum HelpBoxType
    {
        ONE_BUTTON,
        TWO_BUTTONS,
        NONE,
    }

    public sealed class HelpBoxController : MonoBehaviour
    {
        [SerializeField] private RectTransform _helpMenuOneButton;
        [SerializeField] private RectTransform _helpMenuTwoButtons;

        private Sequence _currentSequence;

        public void Raise(HelpBoxType type)
        {            
            _currentSequence?.Kill();
            _currentSequence = DOTween.Sequence();
            _currentSequence.Insert(0f, _helpMenuOneButton.DOAnchorPosY(-50f, 0.2f));
            _currentSequence.Insert(0f, _helpMenuTwoButtons.DOAnchorPosY(-50f, 0.2f));
            
            switch (type)
            {
                case HelpBoxType.ONE_BUTTON:
                    _currentSequence.Insert(0.2f, _helpMenuOneButton.DOAnchorPosY(50f, 0.3f));
                    break;
                case HelpBoxType.TWO_BUTTONS:
                    _currentSequence.Insert(0.2f, _helpMenuTwoButtons.DOAnchorPosY(50f, 0.3f));
                    break;
            }
        }

        public void Remove()
        {
            _currentSequence?.Kill();
            _currentSequence = DOTween.Sequence();
            _currentSequence.Insert(0f, _helpMenuOneButton.DOAnchorPosY(-50f, 0.2f));
            _currentSequence.Insert(0f, _helpMenuTwoButtons.DOAnchorPosY(-50f, 0.2f));
        }

        private void OnDestroy() {
            DOTween.Kill(_currentSequence);
        }
    }
}