using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public static partial class AbilityExtensions
    {
        public static FireAbility SetBox(this FireAbility ability, GameObject box)
        {
            ability.box = box;
            return ability;
        }

        public static FireAbility SetTimerController(this FireAbility ability, TimerController controller)
        {
            ability.timerController = controller;
            return ability;
        }

        public static WaterAbility SetPlayer(this WaterAbility ability, Player player)
        {
            ability.player = player;
            return ability;
        }

        public static TerraAbility SetPlayer(this TerraAbility ability, Player player)
        {
            ability.player = player;
            return ability;
        }

        public static TerraAbility SetEnemies(this TerraAbility ability, UnitStorage unitStorage)
        {
            ability.unitStorage = unitStorage;
            return ability;
        }
    }
}