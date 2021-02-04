namespace DefaultNamespace
{
    public interface ILateExecutable : IControllable
    {
        void LateExecute(float deltaTime);
    }
}