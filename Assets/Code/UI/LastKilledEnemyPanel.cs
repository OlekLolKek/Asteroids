using System.Collections;
using DefaultNamespace;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    public class LastKilledEnemyPanel : BaseUI
    {
        [SerializeField] private RectTransform _thisRectTransform;
        [SerializeField] private Text _text;
        [SerializeField] private string _message;
        [SerializeField] private float _hideDelay;
        [SerializeField] private float _tweenTime;
        [SerializeField] private float _shownPosition;
        [SerializeField] private float _hiddenPotision;
        
        public override void Execute()
        {
            gameObject.SetActive(true);
        }

        public override void Close()
        {
            gameObject.SetActive(false);
        }

        public void SetKilledEnemy(BaseEnemyController enemy)
        {
            _text.text = _message + enemy.Name;
            Hide().ToObservable().Subscribe();
        }

        private IEnumerator Hide()
        {
            transform.DOMoveY(_shownPosition, _tweenTime);
            yield return new WaitForSeconds(_hideDelay);
            transform.DOMoveY(_hiddenPotision, _tweenTime);
        }
    }
}