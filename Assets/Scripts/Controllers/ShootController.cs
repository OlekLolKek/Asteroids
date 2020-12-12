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
        
        public ShootController(IInputKeyPress inputShoot, PlayerData playerData, Transform barrelTransform)
        {
            _inputShoot = inputShoot;
            _barrelTransform = barrelTransform;
            _bulletPrefab = playerData.BulletPrefab;
            _shootForce = playerData.ShootForce;
            _bulletLifespan = playerData.BulletLifespan;
            _inputShoot.OnKeyPressed += OnShootButtonPressed;
        }

        private void OnShootButtonPressed(bool b)
        {
            var bullet = Object.Instantiate(_bulletPrefab, _barrelTransform.position, _barrelTransform.rotation, _barrelTransform);
            bullet.transform.localPosition = new Vector3(bullet.transform.localPosition.x, bullet.transform.localPosition.y + 1);
            bullet.transform.SetParent(null);
            bullet.AddForce(_barrelTransform.up * _shootForce);
            Object.Destroy(bullet.gameObject, _bulletLifespan);
        }
    }
}