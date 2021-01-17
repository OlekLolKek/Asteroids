using UnityEngine;
using UnityEngine.UI;


namespace Command
{
    public class ScorePanel : BaseUI
    {
        [SerializeField] private Text _text;
        
        public override void Execute()
        {
            _text.text = nameof(ScorePanel);
            gameObject.SetActive(true);
        }

        public override void Close()
        {
            gameObject.SetActive(false);
        }
    }
}