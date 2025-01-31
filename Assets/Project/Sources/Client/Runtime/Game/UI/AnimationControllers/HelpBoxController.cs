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
        private bool _raised = false;

        public void SetActive(bool active)
        {
            _helpMenuOneButton.gameObject.SetActive(active);
            _helpMenuTwoButtons.gameObject.SetActive(active);
        }

        public void Raise(HelpBoxType type)
        {            
            _currentSequence?.Kill();
            _currentSequence = DOTween.Sequence();

            if (_raised)
            {
                _currentSequence.Insert(0f, _helpMenuOneButton.DOAnchorPosY(-50f, 0.2f));
                _currentSequence.Insert(0f, _helpMenuTwoButtons.DOAnchorPosY(-50f, 0.2f));
            }

            float startTime = _raised ? 0.1f : 0.0f;
            switch (type)
            {
                case HelpBoxType.ONE_BUTTON:
                    _currentSequence.Insert(startTime, _helpMenuOneButton.DOAnchorPosY(50f, 0.2f).SetEase(Ease.OutQuad));
                    break;
                case HelpBoxType.TWO_BUTTONS:
                    _currentSequence.Insert(startTime, _helpMenuTwoButtons.DOAnchorPosY(50f, 0.2f).SetEase(Ease.OutQuad));
                    break;
            }

            _raised = true;
        }

        public void Remove()
        {
            _currentSequence?.Kill();
            _currentSequence = DOTween.Sequence();
            _currentSequence.Insert(0f, _helpMenuOneButton.DOAnchorPosY(-50f, 0.2f));
            _currentSequence.Insert(0f, _helpMenuTwoButtons.DOAnchorPosY(-50f, 0.2f));

            _raised = false;
        }

        private void OnDestroy() {
            DOTween.Kill(_currentSequence);
        }
    }
}