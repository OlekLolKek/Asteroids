using System;
using UnityEngine;

namespace DefaultNamespace
{
    public sealed class PCInputAxis : IInputChangeAxis
    {
        public event Action<float> OnAxisChanged = delegate(float f) {  };
        private string _axisName;
        
        public PCInputAxis(string axisName)
        {
            _axisName = axisName;
        }
        
        public void GetAxis()
        {
            OnAxisChanged.Invoke(Input.GetAxisRaw(_axisName));
        }
    }
}