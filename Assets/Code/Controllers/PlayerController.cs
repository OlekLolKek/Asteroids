using System.Collections.Generic;
using System.Linq;
using Abilities;
using UnityEngine;

namespace DefaultNamespace
{
    public sealed class PlayerController : IExecutable, ICleanable
    {
        private readonly Controllers _controllers;

        public PlayerController(Data data, InputModel inputModel,
            PlayerModel playerModel)
        {
            _controllers = new Controllers();
            
            var cameraFactory = new CameraFactory(data.CameraData);
            var laserFactory = new LaserFactory();
            
            var cameraModel = new CameraModel(cameraFactory);

            var moveController = new MoveController(inputModel.GetInputKeyboard(),
                data.PlayerData, playerModel.Transform);
            
            var shootController = new ShootController(data.BulletData, playerModel, laserFactory);
            
            var cameraController = new CameraController(cameraModel, playerModel,
                data.CameraData);

            var abilityController = new AbilityController(inputModel, new List<IAbility>
            {
                new Ability("Explosion", 100, Target.None, DamageType.Energy),
                new Ability("Force Field", 0, Target.None, DamageType.None),
                new Ability("Rocket Launcher", 500, Target.None | Target.Aoe, DamageType.Physical)
            });
            
            Debug.Log(abilityController[0]);
            Debug.Log(abilityController[Target.None | Target.Aoe]);
            Debug.Log(abilityController[Target.Passive | Target.None]);
            Debug.Log(abilityController.MaxDamage);
            foreach (var o in abilityController)
            {
                Debug.Log(o);
            }

            foreach (var o in abilityController.GetAbility().Take(2))
            {
                Debug.Log(o);
            }

            foreach (var o in abilityController.GetAbility(DamageType.Energy))
            {
                Debug.Log(o);
            }

            _controllers.Add(moveController).Add(shootController).
                Add(cameraController).Initialize();
        }
        
        public void Execute(float deltaTime)
        {
            _controllers.Execute(deltaTime);
        }

        public void Cleanup()
        {
            _controllers.Cleanup();
        }
    }
}