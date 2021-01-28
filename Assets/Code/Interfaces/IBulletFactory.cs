using UnityEngine;


namespace DefaultNamespace
{
    public interface IBulletFactory
    {
        (GameObject instance, BulletCollision collision, Rigidbody2D rigidbody2D) Create(BulletData data);
    }
}