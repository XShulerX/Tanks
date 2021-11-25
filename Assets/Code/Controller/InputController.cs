using System;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class InputController: IExecute
    {
        public event Action<int> AbilityKeyIsPressed = delegate (int id) {};
        public event Action SaveKeyIsPressed = delegate () { };
        public event Action LoadKeyIsPressed = delegate () { };

        private readonly Dictionary<int,KeyCode> _inputMatching;

        public InputController(Dictionary<int, KeyCode> inputMatching)
        {
            _inputMatching = inputMatching;
        }

        public void CheckAbilityKey()
        {
            foreach (var ability in _inputMatching)
            {
                if (Input.GetKeyDown(ability.Value))
                {
                    AbilityKeyIsPressed.Invoke(ability.Key);
                }
            }
        }

        public void Execute(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SaveKeyIsPressed.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                LoadKeyIsPressed.Invoke();
            }
        }
    }
}