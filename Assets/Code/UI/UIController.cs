using System;
using System.Collections.Generic;
using System.Numerics;
using DefaultNamespace;
using Object = UnityEngine.Object;


namespace UI
{
    public sealed class UIController : ICleanable
    {
        
        private readonly PointModel _pointModel;
        private readonly EnemyPool _enemyPool;
        
        private readonly LastKilledEnemyPanel _lastKilledEnemyPanel;
        private readonly ScorePanel _scorePanel;
        private readonly PausePanel _pausePanel;
        private readonly NullPanel _nullPanel;

        private readonly IInputKeyPress _pause;
        
        private BaseUI _currentPanel;


        public UIController(InputModel inputModel, PointModel pointModel, 
            EnemyPool enemyPool, UIModel uiModel)
        {
            _pause = inputModel.Pause();
            _pause.OnKeyPressed += OnPauseKeyPressed;

            _pointModel = pointModel;
            _pointModel.OnPointsChanged += OnPointsChanged;

            _enemyPool = enemyPool;
            _enemyPool.OnEnemyKilledAndReturned += OnEnemyKilled;

            _lastKilledEnemyPanel = uiModel.LastKilledEnemyPanel;
            _scorePanel = uiModel.ScorePanel;
            _pausePanel = uiModel.PausePanel;
            _nullPanel = uiModel.NullPanel;
            
            _scorePanel.SetText("0");
            _pausePanel.Close();
            _pausePanel.OnResumeButtonPressed += ChangePanel;

            _currentPanel = _nullPanel;
        }

        private void ChangePanel(UiStates uiStates)
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
        
        private void OnEnemyKilled(BaseEnemyController enemy)
        {
            _lastKilledEnemyPanel.SetKilledEnemy(enemy);
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
            _enemyPool.OnEnemyKilledAndReturned -= OnEnemyKilled;
        }
    }
}