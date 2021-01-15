using System;


namespace DefaultNamespace
{
    public interface IInputChangeAxis
    {
        event Action<float> OnAxisChanged;
        void GetAxis();
    }
}