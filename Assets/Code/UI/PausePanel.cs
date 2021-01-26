using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    public sealed class PausePanel : BasePanel
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Image _image;
        [SerializeField] private float _shownPosition;
        [SerializeField] private float _hiddenPosition;
        [SerializeField] private float _tweenTime;


        public RectTransform RectTransform => _rectTransform;
        public Button ResumeButton => _resumeButton;
        public Button ExitButton => _exitButton;
        public Canvas Canvas => _canvas;
        public Image Image => _image;
        public float ShownPosition => _shownPosition;
        public float HiddenPosition => _hiddenPosition;
        public float TweenTime => _tweenTime;
    }
}