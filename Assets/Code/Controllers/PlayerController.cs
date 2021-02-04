using Abilities;
using DefaultNamespace;
using UI;


namespace Controllers
{
    public sealed class PlayerController : IExecutable, ICleanable
    {
        private readonly ControllerList _controllerList;

        public PlayerController(Data data, InputModel inputModel,
            PlayerModel playerModel, PauseModel pauseModel)
        {
            _controllerList = new ControllerList();
            
            var cameraFactory = new CameraFactory(data.CameraData);
            var laserFactory = new LaserFactory();
            
            var cameraModel = new CameraModel(cameraFactory);

            var moveController = new MoveController(inputModel.GetInputKeyboard(),
                data.PlayerData, playerModel.Transform);
            
            var shootController = new ShootController(data.BulletData, playerModel, laserFactory);
            
            var cameraController = new CameraController(cameraModel, playerModel,
                data.CameraData, pauseModel);

            var explosion = new Explosion(data.ExplosionData, playerModel);
            
            var abilityController = new AbilityController(inputModel, explosion);

            _controllerList.Add(moveController).Add(shootController).
                Add(cameraController).Add(abilityController).Initialize();
        }
        
        public void Execute(float deltaTime)
        {
            _controllerList.Execute(deltaTime);
        }

        public void Cleanup()
        {
            _controllerList.Cleanup();
        }
    }
}