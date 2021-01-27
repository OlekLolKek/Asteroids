﻿using System;
using DefaultNamespace;
using DG.Tweening;
using UI.PausePanelTween;
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

        private ISidePanelElementTween[] _elementTweens;
        private Sequence _moveSequence;

        private readonly PauseModel _pauseModel;
        private readonly Button _resumeButton;
        private readonly Button _exitButton;
        private readonly float _tweenTime;

        public Canvas Canvas { get; }
        public Image Image { get; }


        public PausePanelController(PauseModel pauseModel)
        {
            _view = Object.FindObjectOfType<PausePanel>();
            
            _elementTweens = new ISidePanelElementTween[]
            {
                new SidePanelTween(_view.RectTransform),
            };

            _pauseModel = pauseModel;
            ReadyToPause += _pauseModel.Pause;
            OnPanelSwitched += _pauseModel.PausePanelSwitched;
            
            _tweenTime = _view.TweenTime;
            Canvas = _view.Canvas;
            Image = _view.Image;
            _resumeButton = _view.ResumeButton;
            _exitButton = _view.ExitButton;

            _resumeButton.onClick.AddListener(Resume);
            _exitButton.onClick.AddListener(Exit);
        }

        public void Initialize()
        {
            _elementTweens.ForEach(t => t.GoToEnd(MoveMode.Hide));
        }

        public override void Execute()
        {
            _elementTweens = new ISidePanelElementTween[]
            {
                new SidePanelTween(_view.RectTransform),
            };
            
            ShowPausePanel();
        }

        private Sequence Move(MoveMode mode)
        {
            //TODO: add the touchBlocker if some bugs appear

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
            OnResumeButtonPressed.Invoke(UiStates.None);
        }

        private void Exit()
        {
            Close();
        }

        private void HidePausePanel()
        {
            Move(MoveMode.Hide).AppendCallback(() => { _view.gameObject.SetActive(false); });
        }

        private void ShowPausePanel()
        {
            Move(MoveMode.Show).AppendCallback(() => { _view.gameObject.SetActive(true); });
            _elementTweens.ForEach(t => t.GoToEnd(MoveMode.Show));
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