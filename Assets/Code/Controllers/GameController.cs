using DefaultNamespace;
using UI;
using UnityEngine;


namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Data _data;
        private ControllerList _controllerList;


        private void Start()
        {
            _controllerList = new ControllerList();
            
            var asteroidFactory = new AsteroidFactory();
            var playerFactory = new PlayerFactory(_data.PlayerData);

            var playerModel = new PlayerModel(playerFactory);
            var inputModel = new InputModel();
            var pointModel = new PointModel();
            var pauseModel = new PauseModel();
            
            var enemyPool = new EnemyPool(
                _data.EnemyData.AsteroidPoolSize, _data.EnemyData, 
                asteroidFactory);
            
            _controllerList.Add(new InputController(
                inputModel.GetInputKeyboard(), inputModel.GetInputMouse(),
                inputModel.Pause(), inputModel.Ability()));

            _controllerList.Add(new PlayerController(
                _data, inputModel, 
                playerModel, pauseModel));

            _controllerList.Add(new AsteroidController(
                _data.EnemyData, playerModel, 
                pointModel, asteroidFactory,
                enemyPool));

            _controllerList.Add(new UIController(
                inputModel, pointModel, 
                enemyPool, pauseModel));

            _controllerList.Add(new PauseController(
                pauseModel));

            _controllerList.Initialize();
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _controllerList.Execute(deltaTime);
        }

        private void LateUpdate()
        {
            var deltaTime = Time.deltaTime;
            _controllerList.LateExecute(deltaTime);
        }

        private void OnDestroy()
        {
            _controllerList.Cleanup();
        }
    }
}