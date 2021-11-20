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

        public GameObject Panel { get => _panel; }
        public Button RestartButton { get => _nextButton; }
        public Button ExitButton { get => _exitButton; }
    }
}