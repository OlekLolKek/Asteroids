using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    public sealed class ScorePanelController : BasePanelController
    {
        private readonly ScorePanel _view;
        private Text _text;
        
        public ScorePanelController()
        {
            _view = Object.FindObjectOfType<ScorePanel>();
            _text = _view.Text;
        }
        
        public override void Execute()
        {
            _view.gameObject.SetActive(true);
        }

        public override void Close()
        {
            _view.gameObject.SetActive(false);
        }

        public void SetText(string newText)
        {
            _text.text = newText;
        }
    }
}