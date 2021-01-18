using UnityEngine;
using UnityEngine.UI;


namespace Command
{
    public class ScorePanel : BaseUI
    {
        [SerializeField] private Text _text;
        
        public override void Execute()
        {
            gameObject.SetActive(true);
        }

        public override void Close()
        {
            gameObject.SetActive(false);
        }

        public void SetText(string newText)
        {
            _text.text = newText;
        }
    }
}