using UnityEngine;


namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Data _data;
        private Controllers _controllers;


        private void Start()
        {
            //TODO: Разделить класс на инициализаторы
            
            var playerFactory = new PlayerFactory(_data.PlayerData);
            var asteroidFactory = new AsteroidFactory();
            var cameraFactory = new CameraFactory(_data.CameraData);
            var laserFactory = new LaserFactory();

            var playerModel = new PlayerModel(playerFactory);
            var cameraModel = new CameraModel(cameraFactory);
            var inputModel = new InputModel();
            
            _controllers = new Controllers();
            _controllers.Add(new InputController(inputModel.GetInputKeyboard(), inputModel.GetInputMouse(),
                inputModel.GetInputAccelerate()));
            
            _controllers.Add(new MoveController(inputModel.GetInputKeyboard(), inputModel.GetInputAccelerate(),
                _data.PlayerData, playerModel.Transform));
            
            _controllers.Add(new ShootController(_data.BulletData, playerModel.BarrelTransform, 
                laserFactory, playerFactory.GetAudioSource()));
            
            _controllers.Add(new CameraController(cameraModel, playerModel, 
                _data.CameraData));
            
            _controllers.Add(new AsteroidController(_data.EnemyData, playerModel, 
                asteroidFactory));
            
            _controllers.Initialize();
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _controllers.Execute(deltaTime);
        }

        private void LateUpdate()
        {
            var deltaTime = Time.deltaTime;
            _controllers.LateExecute(deltaTime);
        }

        private void OnDestroy()
        {
            _controllers.Cleanup();
        }
    }
}