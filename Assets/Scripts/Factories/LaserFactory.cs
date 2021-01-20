using UnityEngine;

namespace DefaultNamespace
{
    public class LaserFactory : IBulletFactory
    {
        public Bullet Create(EnemyData data)
        {
            var bullet = Object.Instantiate(Resources.Load<Laser>("Bullet/Laser"));
            return bullet;
        }
    }
}