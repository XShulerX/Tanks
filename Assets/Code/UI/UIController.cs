using System.Collections.Generic;

namespace MVC
{
    internal class UIController: IController, IExecute
    {
        private List<IExecute> _uiExecuteControllerlsList = new List<IExecute>();

        public UIController(UIInitializationModel uiModel, PlayerAbilityController abilityController, GameResetOrEndManager gameResetManager, LoadCommandManager loadCommandManager)
        {
            var uiStateController = new UIAbilityPanelsStateController(new UIAbilityPanelsStateControllerModel(uiModel.GamePanelModel, abilityController));
            _uiExecuteControllerlsList.Add(uiStateController);

            new UILostPanelController(uiModel.LostPanelModel, gameResetManager);
            new UIWinPanelController(uiModel.WinPanelModel, gameResetManager);
            new UIGameOverController(uiModel.GameOverPanelModel, gameResetManager);

            var loadPanelController = new UILoadPanelController(uiModel.LoadingPanelModel, loadCommandManager);
            _uiExecuteControllerlsList.Add(loadPanelController);
        }

        public void Execute(float deltaTime)
        {
            for (int i = 0; i < _uiExecuteControllerlsList.Count; i++)
            {
                _uiExecuteControllerlsList[i].Execute(deltaTime);
            }
        }
    }
}