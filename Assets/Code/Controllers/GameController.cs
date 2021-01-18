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
            
            _controllers.Add(new InputController(inputModel.GetInputKeyboard(), inputModel.GetInputMouse(),
                inputModel.Pause()));

            _controllers.Add(new PlayerController(_data, inputModel, playerModel));

            _controllers.Add(new AsteroidController(_data.EnemyData, playerModel, 
                asteroidFactory));

            var uiController = new UIController(inputModel);

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