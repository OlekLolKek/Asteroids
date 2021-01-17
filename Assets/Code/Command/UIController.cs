using System.Collections.Generic;
using DefaultNamespace;
using UnityEditor.VersionControl;
using UnityEngine;


namespace Command
{
    public class UIController : IInitializable, IExecutable
    {
        private readonly ScorePanel _scorePanel;
        private readonly PausePanel _pausePanel;
        private readonly Stack<UiStates> _uiStates = new Stack<UiStates>();
        private BaseUI _currentPanel;


        public UIController()
        {
            _scorePanel = Object.FindObjectOfType<ScorePanel>();
            _pausePanel = Object.FindObjectOfType<PausePanel>();
        }
        
        public void Initialize()
        {
            _scorePanel.Close();
            _pausePanel.Close();
        }

        private void ChangePanel(UiStates uiStates, bool save = true)
        {
            if (_currentPanel != null)
            {
                _currentPanel.Close();
            }

            switch (uiStates)
            {
                case UiStates.ScorePanel:
                    _currentPanel = _scorePanel;
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

        public void Execute(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangePanel(UiStates.ScorePanel);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChangePanel(UiStates.PausePanel);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_uiStates.Count > 0)
                {
                    ChangePanel(_uiStates.Pop(), false);
                }
            }
        }
    }
}