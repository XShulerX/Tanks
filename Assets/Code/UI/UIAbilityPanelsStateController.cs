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
            var abilities = _model.Abilities;
            for(int i = 0; i < abilities.Count; i++)
            {
                var panelImage = _model.AbilitiesPanelMatching[abilities[i].ElementType].GetComponent<Image>();

                if (abilities[i].IsOnCooldown)
                {
                    panelImage.color = _model.CooldownColor;
                }
                else
                {
                    panelImage.color = abilities[i].ElementType switch
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
