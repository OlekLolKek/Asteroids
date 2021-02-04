using UnityEngine;


namespace DefaultNamespace
{
    public interface IEnemyFactory
    {
        (GameObject instance, EnemyCollision collision, Rigidbody2D rigidbody2D) Create(EnemyData data);
    }
}