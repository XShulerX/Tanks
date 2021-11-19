﻿using UnityEngine;
using UnityEngine.UI;
using System;

namespace MVC
{
    [Serializable]
    public struct LostPanelModel
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;

        public GameObject Panel { get => _panel; }
        public Button RestartButton { get => _restartButton; }
        public Button ExitButton { get => _exitButton; }
    }
}