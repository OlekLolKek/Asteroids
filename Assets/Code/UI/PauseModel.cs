using System;


namespace UI
{
    public class PauseModel
    {
        public event Action<bool> OnApplicationPaused = delegate(bool paused) {  };
        public event Action<bool> OnPausePanelSwitched = delegate(bool b) {  };

        public void Pause(bool isPaused)
        {
            OnApplicationPaused.Invoke(isPaused);
        }

        public void PausePanelSwitched(bool isSwitched)
        {
            OnPausePanelSwitched(isSwitched);
        }
    }
}