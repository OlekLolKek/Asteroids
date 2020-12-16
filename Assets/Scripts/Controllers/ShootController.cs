using UnityEngine;

namespace DefaultNamespace
{
    public class ShootController : IExecutable
    {
        private readonly Rigidbody2D _bulletPrefab;
        private readonly Transform _barrelTransform;
        private readonly float _shootForce;
        private readonly float _bulletLifespan;
        private readonly float _scale;
        private float _shootCooldown;
        private float _timer;

        
        public ShootController(PlayerData playerData, Transform barrelTransform)
        {
            _barrelTransform = barrelTransform;
            Debug.Log(barrelTransform.gameObject.name);
            _bulletPrefab = playerData.BulletPrefab;
            _shootForce = playerData.ShootForce;
            _bulletLifespan = playerData.BulletLifespan;
            _scale = playerData.SpriteScale;
            _shootCooldown = playerData.ShootCooldown;
        }

        private void OnShootButtonPressed(bool b)
        {
            var bullet = Object.Instantiate(_bulletPrefab);
            var transform = bullet.transform;
            transform.localScale = new Vector3(_scale, _scale);
            transform.position = _barrelTransform.position;
            bullet.AddForce(transform.up * _shootForce);
            Object.Destroy(bullet.gameObject, _bulletLifespan);
            
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
                var bullet = Object.Instantiate(_bulletPrefab);
                var transform = bullet.transform;
                transform.localScale = new Vector3(_scale, _scale);
                transform.position = _barrelTransform.position;
                bullet.AddForce(transform.up * _shootForce);
                Object.Destroy(bullet.gameObject, _bulletLifespan);
                // var enemy = _pool.GetEnemy(EnemyTypes.Asteroid);
                // var position = _playerTransform.position;
                // //TODO: Заменить число на поле
                // enemy.transform.position = new Vector3(position.x, position.y + 15.0f);
                // enemy.gameObject.SetActive(true);
            }
        }
    }
}