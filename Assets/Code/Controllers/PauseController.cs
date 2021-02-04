using DefaultNamespace;
using UI;
using UnityEngine;


namespace Controllers
{
    public sealed class PauseController : ICleanable
    {
        private readonly PauseModel _pauseModel;
        
        public PauseController(PauseModel pauseModel)
        {
            _pauseModel = pauseModel;
            _pauseModel.OnApplicationPaused += SwitchPause;
        }

        private void SwitchPause(bool isPaused)
        {
            if (isPaused)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

        public void Cleanup()
        {
            _pauseModel.OnApplicationPaused -= SwitchPause;
        }
    }
}