using ChainOfResponsibility;
using Command;
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
            
            _controllers.Add(new InputController(inputModel.GetInputKeyboard(), inputModel.GetInputMouse(),
                inputModel.Pause()));

            _controllers.Add(new PlayerController(_data, inputModel, playerModel));

            _controllers.Add(new AsteroidController(_data.EnemyData, playerModel, 
                pointModel, asteroidFactory));

            var uiController = new UIController(inputModel, pointModel);

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