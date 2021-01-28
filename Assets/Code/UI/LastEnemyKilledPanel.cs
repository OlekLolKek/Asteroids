using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    public class LastEnemyKilledPanel : BasePanel
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Text _text;
        [SerializeField] private string _message;
        [SerializeField] private float _hideDelay;
        [SerializeField] private float _tweenTime;
        [SerializeField] private float _shownYPivot;
        [SerializeField] private float _hiddenYPivot;

        public RectTransform RectTransform => _rectTransform;
        public Text Text => _text;
        public string Message => _message;
        public float HideDelay => _hideDelay;
        public float TweenTime => _tweenTime;
        public float ShownYPivot => _shownYPivot;
        public float HiddenYPivot => _hiddenYPivot;
    }
}