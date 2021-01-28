using System.Collections;
using Controllers;
using DefaultNamespace;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    public class LastEnemyKilledPanelController : BasePanelController
    {
        private LastEnemyKilledPanel _view;

        private readonly RectTransform _rectTransform;
        private readonly Text _text;
        private readonly string _message;
        private readonly float _hideDelay;
        private readonly float _tweenTime;
        private readonly float _shownYPivot;
        private readonly float _hiddenYPivot;

        public LastEnemyKilledPanelController()
        {
            _view = Object.FindObjectOfType<LastEnemyKilledPanel>();
            _rectTransform = _view.RectTransform;
            _text = _view.Text;
            _message = _view.Message;
            _hideDelay = _view.HideDelay;
            _tweenTime = _view.TweenTime;
            _shownYPivot = _view.ShownYPivot;
            _hiddenYPivot = _view.HiddenYPivot;
        }
        
        public override void Execute()
        {
            _view.gameObject.SetActive(true);
        }

        public override void Close()
        {
            _view.gameObject.SetActive(false);
        }

        public void SetKilledEnemy(BaseEnemyController enemy)
        {
            _text.text = _message + enemy.Name;
            Hide().ToObservable().Subscribe();
        }

        private IEnumerator Hide()
        {
            _rectTransform.DOPivotY(_shownYPivot, _tweenTime);
            _view.transform.DOMoveY(0, _tweenTime);
            yield return new WaitForSeconds(_hideDelay);
            _rectTransform.DOPivotY(_hiddenYPivot, _tweenTime);
            _view.transform.DOMoveY(0, _tweenTime);
        }
    }
}