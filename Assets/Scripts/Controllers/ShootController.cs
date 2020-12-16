using System.Collections;
using UniRx;
using UnityEngine;

namespace DefaultNamespace
{
    public class ShootController : IExecutable
    {
        private readonly Transform _barrelTransform;
        private readonly BulletPool _bulletPool;
        private readonly float _shootForce;
        private readonly float _bulletLifespan;
        private readonly float _scale;
        private float _shootCooldown;
        private float _timer;

        
        public ShootController(BulletData bulletData, Transform barrelTransform, LaserFactory laserFactory)
        {
            _bulletPool = new BulletPool(12, bulletData, laserFactory);
            _barrelTransform = barrelTransform;
            _shootForce = bulletData.ShootForce;
            _bulletLifespan = bulletData.BulletLifespan;
            _scale = bulletData.SpriteScale;
            _shootCooldown = bulletData.ShootCooldown;
        }
        

        public void Execute(float deltaTime)
        {
            Shoot(deltaTime);
        }

        private void Shoot(float deltaTime)
        {
            _timer += deltaTime;
            if (_timer >= _shootCooldown)
            {
                _timer = 0.0f;
                var bullet = _bulletPool.GetBullet(BulletTypes.Laser);
                var transform = bullet.transform;
                var rigidbody = bullet.GetComponent<Rigidbody2D>();
                bullet.gameObject.SetActive(true);
                transform.localScale = new Vector3(_scale, _scale);
                transform.position = _barrelTransform.position;
                rigidbody.AddForce(transform.up * _shootForce);
                var coroutine = ReturnToPool(transform, _bulletLifespan).ToObservable().Subscribe();
            }
        }

        private IEnumerator ReturnToPool(Transform bullet, float delay)
        {
            yield return new WaitForSeconds(delay);
            _bulletPool.ReturnToPool(bullet);
        }
    }
}