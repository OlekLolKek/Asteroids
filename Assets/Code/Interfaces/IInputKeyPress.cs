using System;

namespace DefaultNamespace
{
    public interface IInputKeyPress
    {
        event Action OnKeyPressed;
        void GetKey();
    }
}