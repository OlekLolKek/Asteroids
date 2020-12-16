using UnityEngine;


namespace DefaultNamespace
{
    public class Bullet : MonoBehaviour
    {
        public static IBulletFactory Factory;
        private Transform _poolRoot;

        public Transform PoolRoot
        {
            get
            {
                if (_poolRoot == null)
                {
                    var find = GameObject.Find(NameManager.POOL_BULLETS);
                    _poolRoot = find == null ? null : find.transform;
                }

                return _poolRoot;
            }
        }

        public static Bullet CreateBulletLaser()
        {
            var bullet = Instantiate(Resources.Load<Bullet>(PathManager.BULLET_LASER_PATH));
            return bullet;
        }

        public static Bullet CreateBulletLaserWithPool(BulletPool bulletPool)
        {
            var bullet = bulletPool.GetBullet(BulletTypes.Laser);
            bullet.transform.position = Vector3.one;
            bullet.gameObject.SetActive(true);

            return bullet;
        }

        private void ActivateBullet(Vector3 position, Quaternion rotation)
        {
            transform.localPosition = position;
            transform.localRotation = rotation;
            gameObject.SetActive(true);
            transform.SetParent(null);
        }

        protected void ReturnToPool()
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            gameObject.SetActive(false);
            transform.SetParent(PoolRoot);

            if (!PoolRoot)
            {
                Destroy(gameObject);
            }
        }
    }
}