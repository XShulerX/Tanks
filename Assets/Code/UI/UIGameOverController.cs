﻿using UnityEngine;

namespace MVC
{
    internal class UIGameOverController
    {
        private GameOverPanelModel _model;
        private GameResetManager _resetManager;

        public UIGameOverController(GameOverPanelModel gameOverPanelModel, GameResetManager gameResetManager)
        {
            _model = gameOverPanelModel;
            _resetManager = gameResetManager;
            _resetManager.gameOver += ShowPanel;

            _model.ExitButton.onClick.AddListener(Exit);
        }

        private void Exit()
        {
            Application.Quit();
        }

        private void ShowPanel()
        {
            _model.Panel.SetActive(true);
        }
    }
}