using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;


namespace DefaultNamespace
{
    public class BulletPool
    {
        private readonly Dictionary<BulletTypes, HashSet<Bullet>> _bulletPool;
        private IBulletFactory _factory;
        private readonly int _poolCapacity;
        private readonly BulletData _bulletData;
        private readonly Transform _poolRoot;

        public BulletPool(int poolCapacity, BulletData bulletData, IBulletFactory factory)
        {
            _factory = factory;
            _bulletPool = new Dictionary<BulletTypes, HashSet<Bullet>>();
            _poolCapacity = poolCapacity;
            _bulletData = bulletData;
            if (!_poolRoot)
            {
                _poolRoot = new GameObject(NameManager.POOL_BULLETS).transform;
            }
        }

        public Bullet GetBullet(BulletTypes type)
        {
            Bullet result;
            switch (type)
            {
                case BulletTypes.Laser:
                    result = GetBullet(GetListBullets(type));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, "Не предусмотрен в программе");
            }

            return result;
        }

        private HashSet<Bullet> GetListBullets(BulletTypes type)
        {
            return _bulletPool.ContainsKey(type) ? _bulletPool[type] : _bulletPool[type] = new HashSet<Bullet>();
        }

        private Bullet GetBullet(HashSet<Bullet> bullets)
        {
            var bullet = bullets.FirstOrDefault(a => !a.gameObject.activeSelf);
            if (bullet == null)
            {
                var prefab = Resources.Load<Bullet>(PathManager.BULLET_LASER_PATH);
                for (int i = 0; i < _poolCapacity; i++)
                {
                    var instantiate = Object.Instantiate(prefab);
                    ReturnToPool(instantiate.transform);
                    bullets.Add(instantiate);
                }

                GetBullet(bullets);
            }

            bullet = bullets.FirstOrDefault(a => !a.gameObject.activeSelf);
            return bullet;
        }

        public void ReturnToPool(Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.gameObject.SetActive(false);
            transform.SetParent(_poolRoot);
        }

        public void DeletePool()
        {
            Object.Destroy(_poolRoot.gameObject);
        }
    }
}