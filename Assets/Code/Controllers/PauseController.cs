using UI;
using UnityEngine;


namespace DefaultNamespace
{
    public sealed class PauseController : ICleanable
    {
        private readonly UIModel _uiModel;
        
        public PauseController(UIModel uiModel)
        {
            _uiModel = uiModel;
            _uiModel.PausePanel.ReadyToPause += SwitchPause;
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
            _uiModel.PausePanel.ReadyToPause -= SwitchPause;
        }
    }
}