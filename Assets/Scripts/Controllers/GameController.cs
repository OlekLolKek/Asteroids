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
            
            
            var playerModel = new PlayerModel(playerFactory);
            var inputModel = new InputModel();
            
            _controllers = new Controllers();
            _controllers.Add(new InputController(inputModel.GetInputKeyboard(), inputModel.GetInputMouse(), inputModel.GetInputShoot(), inputModel.GetInputAccelerate()));
            _controllers.Add(new MoveController(inputModel.GetInputKeyboard(), inputModel.GetInputAccelerate(), _data.PlayerData, playerModel.Transform));
            _controllers.Add(new LookController(playerModel.Transform, playerModel.Camera));
            _controllers.Add(new ShootController(inputModel.GetInputShoot(), _data.PlayerData, playerModel.BarrelTransform));
            
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