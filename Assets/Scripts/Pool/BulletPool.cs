using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;


namespace DefaultNamespace
{
    public class BulletPool
    {
        private readonly Dictionary<BulletTypes, HashSet<BaseBulletController>> _bulletPool;
        private readonly IBulletFactory _factory;
        private readonly BulletData _bulletData;
        private readonly Transform _poolRoot;
        private readonly int _poolCapacity;
        private int _id = 0;

        public BulletPool(int poolCapacity, BulletData bulletData, IBulletFactory factory)
        {
            _factory = factory;
            _bulletPool = new Dictionary<BulletTypes, HashSet<BaseBulletController>>();
            _poolCapacity = poolCapacity;
            _bulletData = bulletData;
            if (!_poolRoot)
            {
                _poolRoot = new GameObject(NameManager.POOL_BULLETS).transform;
            }
        }

        public BaseBulletController GetBullet(BulletTypes type)
        {
            BaseBulletController result;
            switch (type)
            {
                case BulletTypes.Laser:
                    result = GetBullet(GetBulletHashSet(type));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, "Не предусмотрен в программе");
            }

            return result;
        }

        private HashSet<BaseBulletController> GetBulletHashSet(BulletTypes type)
        {
            if (!_bulletPool.ContainsKey(type))
            {
                _bulletPool[type] = new HashSet<BaseBulletController>();
            }
            
            return _bulletPool[type];
        }

        private BaseBulletController GetBullet(HashSet<BaseBulletController> bullets)
        {
            var bullet = bullets.FirstOrDefault(a => !a.IsActive);
            
            if (bullet == null)
            {
                for (int i = 0; i < _poolCapacity; i++)
                {
                    var newBullet = new BaseBulletController(_bulletData, _factory);
                    newBullet.ID = _id++;
                    ReturnToPool(newBullet);
                    bullets.Add(newBullet);
                }

                GetBullet(bullets);
            }

            bullet = bullets.FirstOrDefault(a => !a.IsActive);
            return bullet;
        }

        public void ReturnToPool(BaseBulletController bullet)
        {
            bullet.ReturnToPool(_poolRoot);
        }

        public void DeletePool()
        {
            Object.Destroy(_poolRoot.gameObject);
        }
    }
}