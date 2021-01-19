using System;


namespace Abilities
{
    [Flags, Serializable]
    public enum Target
    {
        None    = 0,
        Passive = 1,
        Aoe     = 2,
    }
}