using DefaultNamespace;

namespace ChainHandlers
{
    public class PlayerModifier
    {
        protected PlayerModel _player;
        protected PlayerModifier _nextModifier;
        
        public PlayerModifier(PlayerModel player)
        {
            _player = player;
        }

        public void Add(PlayerModifier modifier)
        {
            if (_nextModifier != null)
            {
                _nextModifier.Add(modifier);
            }
            else
            {
                _nextModifier = modifier;
            }
        }

        public virtual void Handle() => _nextModifier?.Handle();
    }
}