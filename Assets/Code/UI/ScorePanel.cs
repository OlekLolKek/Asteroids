using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    public sealed class ScorePanel : BasePanel
    {
        [SerializeField] private Text _text;

        public Text Text => _text;
    }
}