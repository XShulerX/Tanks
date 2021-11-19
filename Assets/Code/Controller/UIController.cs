using System.Collections.Generic;

namespace MVC
{
    internal class UIController: IController, IExecute
    {
        private List<IExecute> _uiExecuteControllerlsList = new List<IExecute>();

        public UIController(UIInitializationModel uiModel, List<IRechargeableAbility> abilities, UnitStorage unitStorage)
        {
            var uiStateController = new UIAbilityPanelsStateController(new UIAbilityPanelsStateControllerModel(uiModel, abilities));
            _uiExecuteControllerlsList.Add(uiStateController);

            var uiLostPanelController = new UILostPanelController(unitStorage.player);
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