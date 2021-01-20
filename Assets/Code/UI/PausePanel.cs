using UnityEngine;
using UnityEngine.UI;

namespace Command
{
    public class PausePanel : BaseUI
    {
        [SerializeField] private Text _text;
        
        public override void Execute()
        {
            _text.text = nameof(PausePanel);
            gameObject.SetActive(true);
        }

        public override void Close()
        {
            gameObject.SetActive(false);
        }
    }
}