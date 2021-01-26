using DefaultNamespace;
using DG.Tweening;
using UI;
using UnityEngine;


namespace Controllers
{
    public class CameraController : IExecutable, ICleanable
    {
        private readonly Transform _playerTransform;
        private readonly Transform _cameraTransform;
        private readonly Vector3 _cameraPosition;
        private readonly PauseModel _pauseModel;

        private readonly float _playPosition;
        private readonly float _pausePosition;
        private readonly float _tweenTime;


        public CameraController(CameraModel cameraModel, PlayerModel playerFactory,
            CameraData cameraData, PauseModel pauseModel)
        {
            _cameraTransform = cameraModel.CameraTransform;
            
            _playerTransform = playerFactory.Transform;
            
            _cameraPosition = cameraData.CameraPosition;
            _playPosition = cameraData.PlayPosition;
            _pausePosition = cameraData.PausePosition;
            _tweenTime = cameraData.TweenTime;
            
            _pauseModel = pauseModel;
            _pauseModel.OnPausePanelSwitched += SwitchPause;
        }
        
        public void Execute(float deltaTime)
        {
            Move();
        }

        private void Move()
        {
            var newPosition = _cameraTransform.position;
            newPosition.y = _playerTransform.position.y + _cameraPosition.y;
            _cameraTransform.position = newPosition;
        }

        private void SwitchPause(bool isPaused)
        {
            if (isPaused)
            {
                _cameraTransform.DOMoveX(_pausePosition, _tweenTime);
            }
            else
            {
                _cameraTransform.DOMoveX(_playPosition, _tweenTime);
            }
        }
        

        public void Cleanup()
        {
            _pauseModel.OnPausePanelSwitched -= SwitchPause;
        }
    }
}