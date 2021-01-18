namespace DefaultNamespace
{
    public sealed class InputController : IExecutable
    {
        private readonly IInputChangeAxis _horizontal;
        private readonly IInputChangeAxis _vertical;
        private readonly IInputChangeAxis _mouseX;
        private readonly IInputChangeAxis _mouseY;
        private readonly IInputKeyPress _pause;


        public InputController((IInputChangeAxis horizontal, IInputChangeAxis vertical) keyboard,
            (IInputChangeAxis mouseX, IInputChangeAxis mouseY) inputMouse,
            IInputKeyPress pause)
        {
            _horizontal = keyboard.horizontal;
            _vertical = keyboard.vertical;
            _mouseX = inputMouse.mouseX;
            _mouseY = inputMouse.mouseY;
            _pause = pause;
        }

        public void Execute(float deltaTime)
        {
            _horizontal.GetAxis();
            _vertical.GetAxis();
            _mouseX.GetAxis();
            _mouseY.GetAxis();
            
            _pause.GetKey();
        }
    }
}