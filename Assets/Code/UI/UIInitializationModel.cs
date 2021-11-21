using System;
using UnityEngine;

namespace MVC
{
    [Serializable]
    public struct UIInitializationModel
    {
        [SerializeField] private GamePanelModel _gamePanelModel;
        [SerializeField] private LostPanelModel _lostPanelModel;
        [SerializeField] private WinPanelModel _winPanelModel;
        [SerializeField] private GameOverPanelModel _GameOverPanelModel;

        public GamePanelModel GamePanelModel { get => _gamePanelModel; }
        public LostPanelModel LostPanelModel { get => _lostPanelModel; }
        public WinPanelModel WinPanelModel { get => _winPanelModel; }
        public GameOverPanelModel GameOverPanelModel { get => _GameOverPanelModel; }
    }
}
