using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Random = UnityEngine.Random;


namespace Abilities
{
    public class AbilityController
    {
        private List<IAbility> _passiveAbilities;
        private InputModel _inputModel;

        
        #region Properties

        public IAbility this[int index] => _passiveAbilities[index];

        public int MaxDamage => _passiveAbilities.Select(a => a.Damage).Max();

        #endregion
        

        public AbilityController(InputModel inputModel, IActiveAbility activeAbility,
            List<IAbility> passivePassiveAbilities)
        {
            _inputModel = inputModel;
            
            _passiveAbilities = passivePassiveAbilities;
        }

        public IEnumerable<IAbility> GetAbility()
        {
            while (true)
            {
                yield return _passiveAbilities[Random.Range(0, _passiveAbilities.Count)];
            }
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < _passiveAbilities.Count; i++)
            {
                yield return _passiveAbilities[i];
            }
        }
    }
}