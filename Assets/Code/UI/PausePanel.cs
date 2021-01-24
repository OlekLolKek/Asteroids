﻿using System;
using System.Collections;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    public sealed class PausePanel : BaseUI
    {
        public event Action<bool> OnPanelSwitched = delegate(bool b) {  };
        public event Action<bool> ReadyToPause = delegate(bool b) {  };
        public event Action<UiStates> OnResumeButtonPressed = delegate(UiStates states) {  };

        [SerializeField] private Canvas _canvas;
        [SerializeField] private Image _image;
        [SerializeField] private float _shownPosition;
        [SerializeField] private float _hiddenPosition;
        [SerializeField] private float _tweenTime;

        public Canvas Canvas => _canvas;
        public Image Image => _image;
        
        public override void Execute()
        {
            ShowPanel().ToObservable().Subscribe();
            _hiddenPosition = -Image.rectTransform.sizeDelta.x * Canvas.scaleFactor;
        }

        public override void Close()
        {
            HidePanel().ToObservable().Subscribe();
        }

        public void Resume()
        {
            HidePanel().ToObservable().Subscribe();
            OnResumeButtonPressed.Invoke(UiStates.None);
        }

        public void Exit()
        {
            Close();
        }

        public void SetClosedPosition(float position)
        {
            _hiddenPosition = position;
        }

        private IEnumerator HidePanel()
        {
            OnPanelSwitched.Invoke(false);
            
            ReadyToPause.Invoke(false);
            
            yield return 0;
            
            transform.DOMoveX(_hiddenPosition, _tweenTime);
            yield return new WaitForSeconds(_tweenTime);
            gameObject.SetActive(false);
        }

        private IEnumerator ShowPanel()
        {
            OnPanelSwitched.Invoke(true);
            
            gameObject.SetActive(true);
            transform.DOMoveX(_shownPosition, _tweenTime);
            yield return new WaitForSeconds(_tweenTime);

            yield return 0;
            
            ReadyToPause.Invoke(true);
        }
    }
}