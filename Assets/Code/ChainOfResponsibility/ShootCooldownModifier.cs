using DefaultNamespace;
using UnityEngine;


namespace ChainOfResponsibility
{
    public sealed class ShootCooldownModifier : PlayerModifier
    {
        private readonly float _shootCooldown;

        public ShootCooldownModifier(PlayerModel player, float shootCooldown)
            : base(player)
        {
            _shootCooldown = shootCooldown;
        }

        public override void Handle()
        {
            Debug.Log("Handle");
            _player.SetShootCooldown(_shootCooldown);
            base.Handle();
        }
    }
}