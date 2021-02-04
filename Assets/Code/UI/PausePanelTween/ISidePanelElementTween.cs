using DG.Tweening;

namespace UI.PausePanelTween
{
    public interface ISidePanelElementTween
    {
        void GoToEnd(MoveMode mode);
        Sequence Move(MoveMode mode, float timeScale);
    }
}