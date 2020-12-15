using UnityEngine;

namespace DefaultNamespace
{
    public class ShootController : IControllable
    {
        private readonly IInputKeyPress _inputShoot;
        private readonly Rigidbody2D _bulletPrefab;
        private readonly Transform _barrelTransform;
        private readonly float _shootForce;
        private readonly float _bulletLifespan;
        private readonly float _scale;
        
        public ShootController(IInputKeyPress inputShoot, PlayerData playerData, Transform barrelTransform)
        {
            _inputShoot = inputShoot;
            _barrelTransform = barrelTransform;
            Debug.Log(barrelTransform.gameObject.name);
            _bulletPrefab = playerData.BulletPrefab;
            _shootForce = playerData.ShootForce;
            _bulletLifespan = playerData.BulletLifespan;
            _scale = playerData.SpriteScale;
            _inputShoot.OnKeyPressed += OnShootButtonPressed;
        }

        private void OnShootButtonPressed(bool b)
        {
            var bullet = Object.Instantiate(_bulletPrefab);
            bullet.transform.localScale = new Vector3(_scale, _scale);
            bullet.transform.position = _barrelTransform.position;
            bullet.AddForce(bullet.transform.up * _shootForce);
            Object.Destroy(bullet.gameObject, _bulletLifespan);
        }
    }
}