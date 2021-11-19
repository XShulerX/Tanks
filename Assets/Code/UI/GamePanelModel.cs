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

        public Text StepTextField { get => _stepTextField; }
        public GameObject FirePanel { get => _firePanel; }
        public GameObject WaterPanel { get => _waterPanel; }
        public GameObject TerraPanel { get => _terraPanel; }
    }
}