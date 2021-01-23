using UnityEngine;

namespace UI
{
    public class UIModel
    {
        public LastKilledEnemyPanel LastKilledEnemyPanel { get; }
        public ScorePanel ScorePanel { get; }
        public PausePanel PausePanel { get; }
        public NullPanel NullPanel { get; }

        public UIModel()
        {
            LastKilledEnemyPanel = Object.FindObjectOfType<LastKilledEnemyPanel>();
            ScorePanel = Object.FindObjectOfType<ScorePanel>();
            PausePanel = Object.FindObjectOfType<PausePanel>();
            NullPanel = Object.FindObjectOfType<NullPanel>();
        }
    }
}