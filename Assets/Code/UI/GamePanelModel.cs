using UnityEngine;
using UnityEngine.UI;
using System;

namespace MVC
{
    [Serializable]
    public struct GamePanelModel
    {
        [SerializeField] private Text _stepTextField;
        [SerializeField] private GameObject _firePanel;
        [SerializeField] private GameObject _waterPanel;
        [SerializeField] private GameObject _terraPanel;
        [SerializeField] private Button _helpButton;
        [SerializeField] private GameObject _helpPanel;
        [SerializeField] private Button _closeHelpButton;

        public Text StepTextField { get => _stepTextField; }
        public GameObject FirePanel { get => _firePanel; }
        public GameObject WaterPanel { get => _waterPanel; }
        public GameObject TerraPanel { get => _terraPanel; }
        public Button HelpButton { get => _helpButton; }
        public GameObject HelpPanel { get => _helpPanel; }
        public Button CloseHelpButton { get => _closeHelpButton; }
    }
}