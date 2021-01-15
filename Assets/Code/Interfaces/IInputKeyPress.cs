using System;

namespace DefaultNamespace
{
    public interface IInputKeyPress
    {
        event Action<bool> OnKeyPressed;
        void GetKey();
    }
}