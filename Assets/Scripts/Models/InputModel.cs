namespace DefaultNamespace
{
    internal class InputModel
    {
        #region Fields

        private readonly IInputChangeAxis _pcInputHorizontal;
        private readonly IInputChangeAxis _pcInputVertical;
        private readonly IInputChangeAxis _pcInputMouseX;
        private readonly IInputChangeAxis _pcInputMouseY;
        private readonly IInputKeyHold _pcInputAccelerate;

        #endregion

        public InputModel()
        {
            _pcInputHorizontal = new PCInputAxis(AxisManager.HORIZONTAL);
            _pcInputVertical = new PCInputAxis(AxisManager.VERTICAL);
            _pcInputMouseX = new PCInputAxis(AxisManager.MOUSE_X);
            _pcInputMouseY = new PCInputAxis(AxisManager.MOUSE_Y);
            _pcInputAccelerate = new PCInputKeyHold(AxisManager.ACCELERATE);
        }

        public (IInputChangeAxis horizontal, IInputChangeAxis vertical) GetInputKeyboard()
        {
            (IInputChangeAxis horizontal, IInputChangeAxis vertical) result = (_pcInputHorizontal, _pcInputVertical);
            return result;
        }
        
        public (IInputChangeAxis mouseX, IInputChangeAxis mouseY) GetInputMouse()
        {
            (IInputChangeAxis mouseX, IInputChangeAxis mouseY) result = (_pcInputMouseX, _pcInputMouseY);
            return result;
        }

        public IInputKeyHold GetInputAccelerate()
        {
            return _pcInputAccelerate;
        }
    }
}