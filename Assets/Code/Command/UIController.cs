using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;


namespace Command
{
    public class UIController
    {
        private readonly ScorePanel _scorePanel;
        private readonly PausePanel _pausePanel;
        private readonly NullPanel _nullPanel;
        private readonly Stack<UiStates> _uiStates = new Stack<UiStates>();

        private readonly IInputKeyPress _pause;
        
        private BaseUI _currentPanel;


        public UIController(InputModel inputModel)
        {
            _pause = inputModel.Pause();
            _pause.OnKeyPressed += OnPauseKeyPressed;
            
            _scorePanel = Object.FindObjectOfType<ScorePanel>();
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
    }
}