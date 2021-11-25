using System;
using UnityEngine;
using UnityEngine.UI;

namespace MVC
{
    [Serializable]
    public struct LoadingPanelModel
    {
        [SerializeField] private GameObject _loadPanel;
        [SerializeField] private Text _loadText;

        public GameObject LoadPanel { get => _loadPanel; }
        public Text LoadText { get => _loadText; }
    }
}