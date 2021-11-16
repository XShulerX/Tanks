using System;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class InputController
    {
        public event Action<int> AbilityKeyIsPressed = delegate (int id) {};

        private readonly Dictionary<int,KeyCode> _inputMatching;

        public InputController(Dictionary<int, KeyCode> inputMatching)
        {
            _inputMatching = inputMatching;
        }

        public void CheckKey()
        {
            foreach (var ability in _inputMatching)
            {
                if (Input.GetKeyDown(ability.Value))
                {
                    AbilityKeyIsPressed.Invoke(ability.Key);
                }
            }
        }
    }
}