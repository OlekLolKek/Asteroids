using System;
using UnityEngine;


namespace DefaultNamespace
{
    public sealed class PCInputKeyHold : IInputKeyHold
    {
        public event Action<bool> OnKeyHeld = delegate(bool b) {  };
        private KeyCode _keyCode;
        
        public PCInputKeyHold(KeyCode keyCode)
        {
            _keyCode = keyCode;
        }
        
        public void GetKey()
        {
            if (Input.GetKey(_keyCode))
            {
                OnKeyHeld.Invoke(true);
            }
            else
            {
                OnKeyHeld.Invoke(false);
            }
        }
    }
}