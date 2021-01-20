using UnityEngine;

namespace DefaultNamespace
{
    public interface IFactory
    {
        GameObject Create();
        Transform GetTransform();
    }
}