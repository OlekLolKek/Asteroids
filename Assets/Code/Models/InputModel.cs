namespace DefaultNamespace
{
    public class InputModel
    {
        #region Fields

        private readonly IInputChangeAxis _pcInputHorizontal;
        private readonly IInputChangeAxis _pcInputVertical;
        private readonly IInputChangeAxis _pcInputMouseX;
        private readonly IInputChangeAxis _pcInputMouseY;
        private readonly IInputKeyPress _pcInputPause;
        private readonly IInputKeyPress _pcInputActiveSkill;

        #endregion

        public InputModel()
        {
            _pcInputHorizontal = new PCInputAxis(AxisNames.HORIZONTAL);
            _pcInputVertical = new PCInputAxis(AxisNames.VERTICAL);
            _pcInputMouseX = new PCInputAxis(AxisNames.MOUSE_X);
            _pcInputMouseY = new PCInputAxis(AxisNames.MOUSE_Y);
            _pcInputPause = new PCInputKey(AxisNames.PAUSE);
            _pcInputActiveSkill = new PCInputKey(AxisNames.ABILITY);
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

        public IInputKeyPress Pause()
        {
            return _pcInputPause;
        }

        public IInputKeyPress Ability()
        {
            return _pcInputActiveSkill;
        }
    }
}