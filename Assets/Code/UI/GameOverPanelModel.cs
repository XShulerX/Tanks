using UnityEngine;
using UnityEngine.UI;
using System;

namespace MVC
{
    [Serializable]
    public struct GameOverPanelModel
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _exitButton;

        public GameObject Panel { get => _panel; }
        public Button ExitButton { get => _exitButton; }
    }
}