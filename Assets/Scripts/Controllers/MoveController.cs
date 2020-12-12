using UnityEngine;


namespace DefaultNamespace
{
    public sealed class MoveController : IExecutable, ICleanable
    {
        #region Fields
        
        private Vector3 _move;
        private readonly Transform _playerTransform;
        private readonly PlayerData _playerData;
        private readonly IInputChangeAxis _horizontalInput;
        private readonly IInputChangeAxis _verticalInput;
        private readonly IInputKeyHold _accelerate;

        private float _horizontal;
        private float _vertical;
        private float _acceleration;
        private bool _isAccelerating;
        
        private readonly float _speed;
        private readonly float _defaultSpeed = 1.0f;
        private readonly float _accelerateSpeed;

        #endregion
        
        public MoveController((IInputChangeAxis horizontal, IInputChangeAxis vertical) input, IInputKeyHold accelerate, 
            PlayerData playerData, Transform playerTransform)
        {
            _horizontalInput = input.horizontal;
            _verticalInput = input.vertical;
            _accelerate = accelerate;
            _horizontalInput.OnAxisChanged += OnHorizontalAxisChanged;
            _verticalInput.OnAxisChanged += OnVerticalAxisChanged;
            _accelerate.OnKeyHeld += OnAccelerationKeyPressed;
            _playerData = playerData;
            _speed = playerData.Speed;
            _acceleration = _playerData.Acceleration;
            _accelerateSpeed = _playerData.Acceleration;
            _playerTransform = playerTransform;
        }

        private void OnHorizontalAxisChanged(float value)
        {
            _horizontal = value;
        }

        private void OnVerticalAxisChanged(float value)
        {
            _vertical = value;
        }

        private void OnAccelerationKeyPressed(bool isKeyHeld)
        {
            if (isKeyHeld) 
                _acceleration = _accelerateSpeed;
            else 
                _acceleration = _defaultSpeed;
        }

        public void Execute(float deltaTime)
        {
            Move(deltaTime);
        }

        public void Cleanup()
        {
            _horizontalInput.OnAxisChanged -= OnHorizontalAxisChanged;
            _verticalInput.OnAxisChanged -= OnVerticalAxisChanged;
        }

        private void Move(float deltaTime)
        {
            var speed = deltaTime * _speed;
            _move = (_playerTransform.right * _horizontal + _playerTransform.up * _vertical).normalized * (speed *  _acceleration);
            _playerTransform.localPosition += _move;
        }
    }
}