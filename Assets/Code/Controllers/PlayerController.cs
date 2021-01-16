namespace DefaultNamespace
{
    public sealed class PlayerController : IExecutable, ICleanable
    {
        private Controllers _controllers;

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