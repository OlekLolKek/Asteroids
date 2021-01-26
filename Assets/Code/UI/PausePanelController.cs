using System;
using System.Collections;
using DefaultNamespace;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;


namespace UI
{
    public class PausePanelController : BasePanelController, ICleanable
    {
        private readonly PausePanel _view;
        
        public event Action<bool> OnPanelSwitched = delegate(bool b) {  };
        public event Action<bool> ReadyToPause = delegate(bool b) {  };
        public event Action<UiStates> OnResumeButtonPressed = delegate(UiStates states) {  };

        private readonly Vector2 _outAnchorMin = new Vector2(-1.0f, 0.0f);
        private readonly Vector2 _inAnchorMin = new Vector2(0.0f, 0.0f);
        private readonly Vector2 _outAnchorMax = new Vector2(0.0f, 1.0f);
        private readonly Vector2 _inAnchorMax = new Vector2(1.0f, 1.0f);
        
        private readonly PauseModel _pauseModel;
        private readonly Button _resumeButton;
        private readonly Button _exitButton;
        private readonly float _shownPosition;
        private readonly float _tweenTime;
        private float _hiddenPosition;

        public Canvas Canvas { get; }
        public Image Image { get; }


        public PausePanelController(PauseModel pauseModel)
        {
            _pauseModel = pauseModel;
            ReadyToPause += _pauseModel.Pause;

            OnPanelSwitched += _pauseModel.PausePanelSwitched;
            
            _view = Object.FindObjectOfType<PausePanel>();

            _shownPosition = _view.ShownPosition;
            _hiddenPosition = _view.HiddenPosition;
            _tweenTime = _view.TweenTime;
            Canvas = _view.Canvas;
            Image = _view.Image;
            
            _resumeButton = _view.ResumeButton;
            _exitButton = _view.ExitButton;
            
            _resumeButton.onClick.AddListener(Resume);
            _exitButton.onClick.AddListener(Exit);
        }
        
        public override void Execute()
        {
            ShowPanel().ToObservable().Subscribe();
            _hiddenPosition = -Image.rectTransform.sizeDelta.x * Canvas.scaleFactor;
        }

        public override void Close()
        {
            HidePanel().ToObservable().Subscribe();
        }

        private void Resume()
        {
            HidePanel().ToObservable().Subscribe();
            OnResumeButtonPressed.Invoke(UiStates.None);
        }

        private void Exit()
        {
            Close();
        }

        private IEnumerator HidePanel()
        {
            OnPanelSwitched.Invoke(false);

            ReadyToPause.Invoke(false);
            
            yield return 0;
            
            _view.transform.DOMoveX(_hiddenPosition, _tweenTime);
            yield return new WaitForSeconds(_tweenTime);
            _view.gameObject.SetActive(false);
        }

        private IEnumerator ShowPanel()
        {
            OnPanelSwitched.Invoke(true);
            
            _view.gameObject.SetActive(true);
            _view.transform.DOMoveX(_shownPosition, _tweenTime);
            yield return new WaitForSeconds(_tweenTime);

            yield return 0;
            
            ReadyToPause.Invoke(true);
        }

        public void Cleanup()
        {
            ReadyToPause -= _pauseModel.Pause;
            OnPanelSwitched -= _pauseModel.PausePanelSwitched;
            
            _resumeButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }
    }
}