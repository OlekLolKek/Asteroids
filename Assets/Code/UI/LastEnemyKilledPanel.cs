using System.Collections;
using DefaultNamespace;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    public class LastEnemyKilledPanel : BasePanel
    {
        [SerializeField] private Text _text;
        [SerializeField] private string _message;
        [SerializeField] private float _hideDelay;
        [SerializeField] private float _tweenTime;
        [SerializeField] private float _shownPosition;
        [SerializeField] private float _hiddenPosition;

        public Text Text => _text;
        public string Message => _message;
        public float HideDelay => _hideDelay;
        public float TweenTime => _tweenTime;
        public float ShownPosition => _shownPosition;
        public float HiddenPosition => _hiddenPosition;
    }
}