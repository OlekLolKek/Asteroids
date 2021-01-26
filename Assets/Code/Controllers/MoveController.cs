using DefaultNamespace;
using UnityEngine;


namespace Controllers
{
    public sealed class MoveController : IExecutable, ICleanable
    {
        #region Fields
        
        private Vector3 _move;
        
        private readonly Transform _playerTransform;
        private readonly IInputChangeAxis _horizontalInput;
        private readonly IInputKeyHold _accelerate;

        private float _horizontal;
        private bool _isAccelerating;
        
        private readonly float _maxRight;
        private readonly float _maxLeft;
        private readonly float _speed;

        #endregion
        
        public MoveController((IInputChangeAxis horizontal, IInputChangeAxis vertical) input,
            PlayerData playerData, Transform playerTransform)
        {
            _horizontalInput = input.horizontal;
            _horizontalInput.OnAxisChanged += OnHorizontalAxisChanged;
            _speed = playerData.Speed;
            _playerTransform = playerTransform;
            _maxLeft = playerData.MAXLeft;
            _maxRight = playerData.MAXRight;
        }

        private void OnHorizontalAxisChanged(float value)
        {
            _horizontal = value;
        }
        
        public void Execute(float deltaTime)
        {
            Move(deltaTime);
        }

        public void Cleanup()
        {
            _horizontalInput.OnAxisChanged -= OnHorizontalAxisChanged;
        }

        private void Move(float deltaTime)
        {
            var localPosition = _playerTransform.localPosition;
            var speed = deltaTime * _speed;
            _move = new Vector3(_horizontal * speed, speed) ;

            localPosition += _move;
            localPosition.x = Mathf.Clamp(localPosition.x, _maxLeft, _maxRight);
            _playerTransform.localPosition = localPosition;
        }
    }
}