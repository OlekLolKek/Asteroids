using System;
using System.Collections;
using DefaultNamespace;
using DG.Tweening;
using UI.PausePanelTween;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;


namespace UI
{
    public class PausePanelController : BasePanelController, IInitializable, ICleanable
    {
        private readonly PausePanel _view;

        public event Action<bool> OnPanelSwitched = delegate(bool b) { };
        public event Action<bool> ReadyToPause = delegate(bool b) { };
        public event Action<UiStates> OnResumeButtonPressed = delegate(UiStates states) { };

        private IDisposable _hideCoroutine;
        private IDisposable _showCoroutine;
        private ISidePanelElementTween[] _elementTweens;
        private Sequence _moveSequence;

        private readonly PauseModel _pauseModel;
        private readonly Button _resumeButton;
        private readonly Button _exitButton;
        private readonly float _tweenTime;


        public PausePanelController(PauseModel pauseModel)
        {
            _view = Object.FindObjectOfType<PausePanel>();
            
            _resumeButton = _view.ResumeButton;
            _exitButton = _view.ExitButton;
            _tweenTime = _view.TweenTime;
            
            _elementTweens = new ISidePanelElementTween[]
            {
                new SidePanelTween(_view.RectTransform, _tweenTime),
            };

            _pauseModel = pauseModel;
            ReadyToPause += _pauseModel.Pause;
            OnPanelSwitched += _pauseModel.PausePanelSwitched;
            _resumeButton.onClick.AddListener(Resume);
            _exitButton.onClick.AddListener(Exit);
        }

        public void Initialize()
        {
            _elementTweens.ForEach(t => t.GoToEnd(MoveMode.Hide));
            _view.gameObject.SetActive(false);
        }

        public override void Execute()
        {
            _elementTweens = new ISidePanelElementTween[]
            {
                new SidePanelTween(_view.RectTransform, _tweenTime),
            };
            
            ShowPausePanel();
        }

        private Sequence Move(MoveMode mode)
        {
            float timeScale = 1.0f;
            if (_moveSequence != null)
            {
                timeScale = _moveSequence.position / _moveSequence.Duration();
                _moveSequence.Kill();
            }

            _moveSequence = DOTween.Sequence();
            _elementTweens.ForEach(t => _moveSequence.Join(t.Move(mode, timeScale)));
            _moveSequence.AppendCallback(() => { _moveSequence = null; });

            return _moveSequence;
        }

        public override void Close()
        {
            HidePausePanel();
        }

        private void Resume()
        {
            HidePausePanel();
            ResumeButtonPress().ToObservable().Subscribe();
        }

        private void Exit()
        {
            Application.Quit();
        }

        private IEnumerator ResumeButtonPress()
        {
            yield return new WaitForSeconds(_tweenTime);
            OnResumeButtonPressed.Invoke(UiStates.None);
        }

        private IEnumerator Pause()
        {
            yield return new WaitForSeconds(_tweenTime);
            ReadyToPause.Invoke(true);
        }

        private void HidePausePanel()
        {
            _showCoroutine?.Dispose();
            ReadyToPause.Invoke(false);
            OnPanelSwitched.Invoke(false);
            Move(MoveMode.Hide).AppendCallback(() => { _view.gameObject.SetActive(false); });
        }

        private void ShowPausePanel()
        {
            OnPanelSwitched.Invoke(true);
            _view.gameObject.SetActive(true);
            Move(MoveMode.Show);
            _elementTweens.ForEach(t => t.GoToEnd(MoveMode.Show));
            _showCoroutine = Pause().ToObservable().Subscribe();
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