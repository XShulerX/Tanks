using UnityEngine;
using UnityEngine.UI;
using System;

namespace MVC
{
    [Serializable]
    public struct WinPanelModel
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Text _stagesCount;

        public GameObject Panel { get => _panel; }
        public Button NextButton { get => _nextButton; }
        public Button ExitButton { get => _exitButton; }
        public Text StagesCount { get => _stagesCount; }
    }
}