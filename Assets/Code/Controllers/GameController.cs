using ChainOfResponsibility;
using UI;
using UnityEngine;


namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Data _data;
        private Controllers _controllers;


        private void Start()
        {
            _controllers = new Controllers();
            
            var asteroidFactory = new AsteroidFactory();
            var playerFactory = new PlayerFactory(_data.PlayerData);

            var playerModel = new PlayerModel(playerFactory);
            var inputModel = new InputModel();
            var pointModel = new PointModel();
            var uiModel = new UIModel();
            
            var enemyPool = new EnemyPool(
                _data.EnemyData.AsteroidPoolSize, _data.EnemyData, 
                asteroidFactory);
            
            _controllers.Add(new InputController(
                inputModel.GetInputKeyboard(), inputModel.GetInputMouse(),
                inputModel.Pause(), inputModel.Ability()));

            _controllers.Add(new PlayerController(
                _data, inputModel, 
                playerModel, uiModel));

            _controllers.Add(new AsteroidController(
                _data.EnemyData, playerModel, 
                pointModel, asteroidFactory,
                enemyPool));

            _controllers.Add(new UIController(
                inputModel, pointModel, 
                enemyPool, uiModel));

            _controllers.Add(new PauseController(
                uiModel));

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