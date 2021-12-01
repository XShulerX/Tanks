using System;
using UnityEngine;

namespace MVC
{
    public class UILostPanelController
    {
        private LostPanelModel _model;
        private GameResetOrEndManager _resetManager;

        

        public UILostPanelController(LostPanelModel lostPanelModel, GameResetOrEndManager gameResetManager)
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
            _resetManager.ResetScene(true);
            _model.Panel.SetActive(false);
        }

        private void ShowPanel(int attemptsCount)
        {
            _model.AttemptsCountText.text = String.Concat("Попыток осталось: ", attemptsCount);
            _model.Panel.SetActive(true);
        }
    }
}