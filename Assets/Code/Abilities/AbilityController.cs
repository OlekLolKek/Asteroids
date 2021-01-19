using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Random = UnityEngine.Random;


namespace Abilities
{
    public class AbilityController : IExecutable, ICleanable
    {
        //private List<IAbility> _passiveAbilities;
        private IActiveAbility _activeAbility;
        private InputModel _inputModel;

        
        #region Properties

        //public IAbility this[int index] => _passiveAbilities[index];

        //public int MaxDamage => _passiveAbilities.Select(a => a.Damage).Max();

        #endregion
        

        public AbilityController(InputModel inputModel, IActiveAbility activeAbility)
        {
            _inputModel = inputModel;
            _inputModel.Ability().OnKeyPressed += ActivateAbility;
            
            _activeAbility = activeAbility;
        }

        // public IEnumerable<IAbility> GetAbility()
        // {
        //     while (true)
        //     {
        //         yield return _passiveAbilities[Random.Range(0, _passiveAbilities.Count)];
        //     }
        // }

        // public IEnumerator GetEnumerator()
        // {
        //     for (int i = 0; i < _passiveAbilities.Count; i++)
        //     {
        //         yield return _passiveAbilities[i];
        //     }
        // }

        private void ActivateAbility()
        {
            _activeAbility.Cast();
        }

        public void Execute(float deltaTime)
        {
            _activeAbility.Execute(deltaTime);
        }

        public void Cleanup()
        {
            _inputModel.Ability().OnKeyPressed -= ActivateAbility;
        }
    }
}