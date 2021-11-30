using UnityEngine.UI;

namespace MVC {
    public class UIAbilityPanelsStateController: IExecute
    {
        private UIAbilityPanelsStateControllerModel _model;

        public UIAbilityPanelsStateController(UIAbilityPanelsStateControllerModel model)
        {
            _model = model;
        }

        public void Execute(float deltaTime)
        {
            var abilities = _model.PlayerAbilityController.ActivePlayer.Abilities;
            foreach(var ability in abilities)
            {
                var panelImage = _model.AbilitiesPanelMatching[ability.Value.ElementType].GetComponent<Image>();

                if (ability.Value.IsOnCooldown)
                {
                    panelImage.color = _model.CooldownColor;
                }
                else
                {
                    panelImage.color = ability.Value.ElementType switch
                    {
                        Elements.Fire => _model.FireColor,
                        Elements.Terra => _model.TerraColor,
                        Elements.Water => _model.WaterColor
                    };
                }
            }
        }
    }
}
