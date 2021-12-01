using System;
using UnityEngine;
using UnityEngine.UI;

namespace MVC {
    public class UIGamePanelController: IExecute
    {
        private UIGamePanelControllerModel _model;

        public UIGamePanelController(UIGamePanelControllerModel model)
        {
            _model = model;

            _model.HelpButton.onClick.AddListener(ShowHelpPanel);
            _model.CloseHelpButton.onClick.AddListener(HideHelpPanel);

            _model.InputController.HelpKeyIsPressed += ShowHelpPanel;
        }

        private void HideHelpPanel()
        {
            _model.HelpPanel.SetActive(false);
        }

        private void ShowHelpPanel()
        {
            _model.HelpPanel.SetActive(true);
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
