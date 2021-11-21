using System;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class InputAdapter
    {
        private readonly Dictionary<int, KeyCode> _inputMatching;

        public InputAdapter(List<AbilityModel> abilities)
        {
            _inputMatching = new Dictionary<int, KeyCode>();

            for (int i = 0; i < abilities.Count; i++)
            {
                if (_inputMatching.ContainsKey(abilities[i].AbilitiID))
                {
                    throw new Exception("среди ID умений имеются дубликаты");
                }
                _inputMatching.Add(abilities[i].AbilitiID, abilities[i].Key);
            }
        }

        public Dictionary<int, KeyCode> GetMatching()
        {
            return _inputMatching;
        }
    }
}