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
        
        private readonly Text _text;
        private readonly string _message;
        private readonly float _hideDelay;
        private readonly float _tweenTime;
        private readonly float _shownPosition;
        private readonly float _hiddenPotision;

        public LastEnemyKilledPanelController()
        {
            _view = Object.FindObjectOfType<LastEnemyKilledPanel>();
            _text = _view.Text;
            _message = _view.Message;
            _hideDelay = _view.HideDelay;
            _tweenTime = _view.TweenTime;
            _shownPosition = _view.ShownPosition;
            _hiddenPotision = _view.HiddenPosition;
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
            _view.transform.DOMoveY(_shownPosition, _tweenTime);
            yield return new WaitForSeconds(_hideDelay);
            _view.transform.DOMoveY(_hiddenPotision, _tweenTime);
        }
    }
}