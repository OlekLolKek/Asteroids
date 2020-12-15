using UnityEngine;


namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Data _data;
        private Controllers _controllers;


        private void Start()
        {
            var playerFactory = new PlayerFactory(_data.PlayerData);
            var asteroidFactory = new AsteroidFactory();
            var cameraFactory = new CameraFactory(_data.CameraData);


            var playerModel = new PlayerModel(playerFactory);
            var cameraModel = new CameraModel(cameraFactory);
            var inputModel = new InputModel();
            
            _controllers = new Controllers();
            _controllers.Add(new InputController(inputModel.GetInputKeyboard(), inputModel.GetInputMouse(), inputModel.GetInputShoot(), inputModel.GetInputAccelerate()));
            _controllers.Add(new MoveController(inputModel.GetInputKeyboard(), inputModel.GetInputAccelerate(), _data.PlayerData, playerModel.Transform));
            _controllers.Add(new ShootController(inputModel.GetInputShoot(), _data.PlayerData, playerModel.BarrelTransform));
            _controllers.Add(new CameraController(cameraModel, playerModel, _data.CameraData));

            var enemyPool = new EnemyPool(5, _data.EnemyData, asteroidFactory);
            var enemy = enemyPool.GetEnemy(EnemyTypes.Asteroid);
            enemy.Instance.transform.position = Vector3.one;
            enemy.Instance.SetActive(true);
            
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