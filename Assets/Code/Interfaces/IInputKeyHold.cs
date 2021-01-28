using System;


namespace DefaultNamespace
{
    public interface IInputKeyHold
    {
        event Action<bool> OnKeyHeld;
        void GetKey();
    }
}