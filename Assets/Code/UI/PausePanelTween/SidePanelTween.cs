using DG.Tweening;
using UnityEngine;

namespace UI.PausePanelTween
{
    public sealed class SidePanelTween : ISidePanelElementTween
    {
        private static readonly Vector2 OutAnchorMin = new Vector2(-1.0f, 0.0f);
        private static readonly Vector2 InAnchorMin = new Vector2(0.0f, 0.0f);
        private static readonly Vector2 OutAnchorMax = new Vector2(0.0f, 1.0f);
        private static readonly Vector2 InAnchorMax = new Vector2(1.0f, 1.0f);
        private readonly RectTransform _moveRoot;


        public SidePanelTween(RectTransform moveRoot)
        {
            _moveRoot = moveRoot;
        }


        public void GoToEnd(MoveMode mode)
        {
            switch (mode)
            {
                case MoveMode.Show:
                    _moveRoot.anchorMin = OutAnchorMin;
                    _moveRoot.anchorMax = OutAnchorMax;
                    break;
                case MoveMode.Hide:
                    _moveRoot.anchorMin = InAnchorMin;
                    _moveRoot.anchorMax = InAnchorMax;
                    break;
            }
        }


        public Sequence Move(MoveMode mode, float timeScale)
        {
            Vector2 anchorMin = Vector2.zero;
            Vector2 anchorMax = Vector2.zero;
            switch (mode)
            {
                case MoveMode.Show:
                    anchorMin = InAnchorMin;
                    anchorMax = InAnchorMax;
                    break;
                case MoveMode.Hide:
                    anchorMin = OutAnchorMin;
                    anchorMax = OutAnchorMax;
                    break;
            }
            
            Debug.Log(anchorMin + "     " + anchorMax);

            const float TOTAL_MOVE_DURATION = 0.5f;
            const Ease MOVE_EASE = Ease.InOutBack;
            return DOTween.Sequence()
                .Append(_moveRoot.DOAnchorMin(anchorMin, TOTAL_MOVE_DURATION * timeScale).SetEase(MOVE_EASE))
                .Join(_moveRoot.DOAnchorMax(anchorMax, TOTAL_MOVE_DURATION * timeScale).SetEase(MOVE_EASE));
        }
    }
}