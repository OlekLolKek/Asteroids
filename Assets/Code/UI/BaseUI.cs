using UnityEngine;


namespace Command
{
    public abstract class BaseUI : MonoBehaviour
    {
        public abstract void Execute();
        public abstract void Close();
    }
}