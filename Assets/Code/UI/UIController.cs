using System;
using System.Numerics;
using Controllers;
using DefaultNamespace;


namespace UI
{
    public sealed class UIController : ICleanable
    {
        private readonly ControllerList _controllers;
        
        private readonly PointModel _pointModel;
        private readonly EnemyPool _enemyPool;
        
        private readonly LastEnemyKilledPanelController _lastEnemyKilledPanelController;
        private readonly ScorePanelController _scorePanelController;
        private readonly PausePanelController _pausePanelController;
        private readonly NullPanelController _nullPanelController;

        private readonly IInputKeyPress _pause;
        
        private BasePanelController _currentPanelController;


        public UIController(InputModel inputModel, PointModel pointModel, 
            EnemyPool enemyPool, PauseModel pauseModel)
        {
            _controllers = new ControllerList();
            
            _pause = inputModel.Pause();
            _pause.OnKeyPressed += OnPauseKeyPressed;

            _pointModel = pointModel;
            _pointModel.OnPointsChanged += OnPointsChanged;

            _enemyPool = enemyPool;
            _enemyPool.OnEnemyKilledAndReturned += OnEnemyKilled;

            _lastEnemyKilledPanelController = new LastEnemyKilledPanelController();
            _scorePanelController = new ScorePanelController();
            _pausePanelController = new PausePanelController(pauseModel);
            _nullPanelController = new NullPanelController();

            _controllers.Add(_pausePanelController);
            
            _scorePanelController.SetText("0");
            _pausePanelController.Close();
            _pausePanelController.OnResumeButtonPressed += ChangePanelController;

            _currentPanelController = _nullPanelController;
            
            _controllers.Initialize();
        }

        private void ChangePanelController(UiStates uiStates)
        {
            _currentPanelController?.Close();

            switch (uiStates)
            {
                case UiStates.None:
                    _currentPanelController = _nullPanelController;
                    break;
                case UiStates.PausePanel:
                    _currentPanelController = _pausePanelController;
                    break;
            }
            
            _currentPanelController?.Execute();
        }

        private void OnPauseKeyPressed()
        {
            if (_currentPanelController != _nullPanelController)
            {
                ChangePanelController(UiStates.None);
            }
            else
            {
                ChangePanelController(UiStates.PausePanel);
            }
        }
        
        private void OnEnemyKilled(BaseEnemyController enemy)
        {
            _lastEnemyKilledPanelController.SetKilledEnemy(enemy);
        }
        
        private void OnPointsChanged(BigInteger points)
        {
            _scorePanelController.SetText(PointsToAbbreviation(points));
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
            _pausePanelController.OnResumeButtonPressed += ChangePanelController;
        }
    }
}