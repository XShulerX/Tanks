using System;
using UnityEngine;

namespace MVC
{
    public class UILostPanelController
    {
        private LostPanelModel _model;
        private GameResetManager _resetManager;

        

        public UILostPanelController(LostPanelModel lostPanelModel, GameResetManager gameResetManager)
        {
            _model = lostPanelModel;
            _resetManager = gameResetManager;
            _resetManager.lostGame += ShowPanel;

            _model.RestartButton.onClick.AddListener(Restart);
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

        private void ShowPanel(int attemptsCount)
        {
            _model.AttemptsCountText.text = String.Concat("Попыток осталось: ", attemptsCount);
            _model.Panel.SetActive(true);
        }
    }
}