using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Random = UnityEngine.Random;


namespace Abilities
{
    public sealed class AbilityController : IExecutable, ICleanable
    {
        private readonly IActiveAbility _activeAbility;
        private readonly InputModel _inputModel;


        public AbilityController(InputModel inputModel, IActiveAbility activeAbility)
        {
            _inputModel = inputModel;
            _inputModel.Ability().OnKeyPressed += ActivateAbility;
            
            _activeAbility = activeAbility;
        }

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