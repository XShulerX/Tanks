using System.Collections.Generic;

namespace MVC
{
    internal class UIController: IController, IExecute
    {
        private List<IExecute> _uiExecuteControllerlsList = new List<IExecute>();

        public UIController(UIInitializationModel uiModel, Dictionary<int, Ability> abilities, UnitStorage unitStorage, GameResetManager gameResetManager)
        {
            var uiAdapter = new UIAdapter(abilities);
            var uiStateController = new UIAbilityPanelsStateController(new UIAbilityPanelsStateControllerModel(uiModel.GamePanelModel, uiAdapter.GetAbilities()));
            _uiExecuteControllerlsList.Add(uiStateController);

            var uiLostPanelController = new UILostPanelController(uiModel.LostPanelModel, gameResetManager);
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