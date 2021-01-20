﻿namespace DefaultNamespace
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
            _pcInputHorizontal = new PCInputAxis(AxisManager.HORIZONTAL);
            _pcInputVertical = new PCInputAxis(AxisManager.VERTICAL);
            _pcInputMouseX = new PCInputAxis(AxisManager.MOUSE_X);
            _pcInputMouseY = new PCInputAxis(AxisManager.MOUSE_Y);
            _pcInputPause = new PCInputKey(AxisManager.PAUSE);
            _pcInputActiveSkill = new PCInputKey(AxisManager.ABILITY);
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