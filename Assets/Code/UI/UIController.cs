using System;
using System.Collections.Generic;
using System.Numerics;
using DefaultNamespace;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Command
{
    public class UIController : ICleanable
    {
        private readonly PointModel _pointModel;
        private readonly ScorePanel _scorePanel;
        private readonly PausePanel _pausePanel;
        private readonly NullPanel _nullPanel;
        private readonly Stack<UiStates> _uiStates = new Stack<UiStates>();

        private readonly IInputKeyPress _pause;
        
        private BaseUI _currentPanel;


        public UIController(InputModel inputModel, PointModel pointModel)
        {
            _pause = inputModel.Pause();
            _pause.OnKeyPressed += OnPauseKeyPressed;

            _pointModel = pointModel;
            _pointModel.OnPointsChanged += OnPointsChanged;

            _scorePanel = Object.FindObjectOfType<ScorePanel>();
            _scorePanel.SetText("0");
            _pausePanel = Object.FindObjectOfType<PausePanel>();
            _nullPanel = Object.FindObjectOfType<NullPanel>();
            
            _pausePanel.Close();

            _currentPanel = _nullPanel;
        }

        private void ChangePanel(UiStates uiStates, bool save = true)
        {
            if (_currentPanel != null)
            {
                _currentPanel.Close();
            }

            switch (uiStates)
            {
                case UiStates.None:
                    _currentPanel = _nullPanel;
                    break;
                case UiStates.PausePanel:
                    _currentPanel = _pausePanel;
                    break;
            }
            
            _currentPanel.Execute();
            if (save)
            {
                _uiStates.Push(uiStates);
            }
        }

        private void OnPauseKeyPressed()
        {
            if (_currentPanel != _nullPanel)
            {
                ChangePanel(UiStates.None);
            }
            else
            {
                ChangePanel(UiStates.PausePanel);
            }
        }

        private void OnPointsChanged(BigInteger points)
        {
            _scorePanel.SetText(PointsToAbbreviation(points));
        }

        private string PointsToAbbreviation(BigInteger points)
        {
            if (points < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(points), "The amount of points can not be negative.");
            }
            
            if (points == 0) return "0";
            if (points >= 1000000000000) return "Over9000";
            if (points >= 1000000000) return ($"{(float)points / 1000000000:f1}B");
            if (points >= 1000000) return $"{(float)points / 1000000:f1}M";
            if (points >= 1000) return $"{(float)points / 1000:f1}K";

            return points.ToString();
        }

        public void Cleanup()
        {
            _pause.OnKeyPressed -= OnPauseKeyPressed;
            _pointModel.OnPointsChanged -= OnPointsChanged;
        }
    }
}