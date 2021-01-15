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
        private readonly IInputKeyHold _accelerate;

        private float _horizontal;
        private float _acceleration;
        private bool _isAccelerating;
        
        private readonly float _speed;
        private readonly float _defaultSpeed = 1.0f;
        private readonly float _accelerateSpeed;
        private readonly float _maxLeft;
        private readonly float _maxRight;

        #endregion
        
        public MoveController((IInputChangeAxis horizontal, IInputChangeAxis vertical) input, IInputKeyHold accelerate, 
            PlayerData playerData, Transform playerTransform)
        {
            _horizontalInput = input.horizontal;
            _accelerate = accelerate;
            _horizontalInput.OnAxisChanged += OnHorizontalAxisChanged;
            _accelerate.OnKeyHeld += OnAccelerationKeyPressed;
            _playerData = playerData;
            _speed = playerData.Speed;
            _acceleration = _playerData.Acceleration;
            _accelerateSpeed = _playerData.Acceleration;
            _playerTransform = playerTransform;
            _maxLeft = playerData.MAXLeft;
            _maxRight = playerData.MAXRight;
        }

        private void OnHorizontalAxisChanged(float value)
        {
            _horizontal = value;
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
        }

        private void Move(float deltaTime)
        {
            var localPosition = _playerTransform.localPosition;
            var speed = deltaTime * _speed;
            _move = new Vector3(_horizontal * (speed * _acceleration), speed) ;

            localPosition += _move;
            localPosition.x = Mathf.Clamp(localPosition.x, _maxLeft, _maxRight);
            _playerTransform.localPosition = localPosition;
        }
    }
}