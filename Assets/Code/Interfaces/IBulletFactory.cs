using UnityEngine;

namespace DefaultNamespace
{
    public interface IBulletFactory
    {
        GameObject Create(BulletData data);
    }
}