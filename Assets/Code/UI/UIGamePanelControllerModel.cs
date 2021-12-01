using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MVC
{
    public class UIGamePanelControllerModel
    {
        private readonly Dictionary<Elements, GameObject> _abilitiesPanelMatching;
        private PlayerAbilityController _playerAbilityController;
        private readonly Color _cooldownColor = new Color(1f, 0.9712f, 0.9669f, 1f);
        private readonly Color _fireColor = new Color(1f, 0.1314479f, 0f, 1f);
        private readonly Color _waterColor = new Color(0f, 0.1708491f, 1f, 1f);
        private readonly Color _terraColor = new Color(0.3584906f, 0.1866689f, 0f, 1f);

        private Button _helpButton;
        private GameObject _helpPanel;
        private Button _closeHelpButton;
        private InputController _inputController;

        public Dictionary<Elements, GameObject> AbilitiesPanelMatching => _abilitiesPanelMatching;
        public Color CooldownColor => _cooldownColor;
        public Color FireColor => _fireColor;
        public Color WaterColor => _waterColor;
        public Color TerraColor => _terraColor;

        public PlayerAbilityController PlayerAbilityController { get => _playerAbilityController; }
        public Button HelpButton { get => _helpButton; }
        public GameObject HelpPanel { get => _helpPanel; }
        public Button CloseHelpButton { get => _closeHelpButton; }
        public InputController InputController { get => _inputController; }

        public UIGamePanelControllerModel(GamePanelModel model, PlayerAbilityController playerAbilityController, InputController inputController)
        {
            _abilitiesPanelMatching = new Dictionary<Elements, GameObject>
            {
                [Elements.Water] = model.WaterPanel,
                [Elements.Fire] = model.FirePanel,
                [Elements.Terra] = model.TerraPanel
            };
            _playerAbilityController = playerAbilityController;
            _helpButton = model.HelpButton;
            _helpPanel = model.HelpPanel;
            _closeHelpButton = model.CloseHelpButton;
            _inputController = inputController;
        }
    }
}
