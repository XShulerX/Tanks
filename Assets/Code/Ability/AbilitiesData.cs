using System.Collections.Generic;
using UnityEngine;

namespace MVC {
    [CreateAssetMenu(menuName = "Data/Abilities", fileName = nameof(AbilitiesData))]
    public class AbilitiesData: ScriptableObject
    {
        [SerializeField] private List<AbilityModel> _abilitiesModel;

        public List<AbilityModel> AbilitiesModel { get => _abilitiesModel; }
    }
}