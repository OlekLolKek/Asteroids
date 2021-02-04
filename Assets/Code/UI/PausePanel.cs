using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    public sealed class PausePanel : BasePanel
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private float _tweenTime;


        public RectTransform RectTransform => _rectTransform;
        public Button ResumeButton => _resumeButton;
        public Button ExitButton => _exitButton;
        public float TweenTime => _tweenTime;
    }
}