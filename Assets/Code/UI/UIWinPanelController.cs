using System;
using UnityEngine;

namespace MVC
{
    internal class UIWinPanelController
    {
        private WinPanelModel _model;
        private GameResetOrEndManager _resetManager;

        public UIWinPanelController(WinPanelModel winPanelModel, GameResetOrEndManager gameResetManager)
        {
            _model = winPanelModel;
            _resetManager = gameResetManager;
            _resetManager.winGame += ShowPanel;

            _model.NextButton.onClick.AddListener(Restart);
            _model.ExitButton.onClick.AddListener(Exit);
        }

        private void Exit()
        {
            Application.Quit();
        }

        private void Restart()
        {
            _resetManager.ResetScene();
            _model.Panel.SetActive(false);
        }

        private void ShowPanel(int stageCount)
        {
            _model.StagesCount.text = String.Concat("Уровней пройдено: ", stageCount);
            _model.Panel.SetActive(true);
        }
    }
}