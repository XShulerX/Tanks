using UnityEngine;

namespace MVC
{
    internal class UILoadPanelController
    {
        private GameObject _loadPanel;
        private LoadCommandManager _loadCommandManager;

        public UILoadPanelController(GameObject loadPanel, LoadCommandManager loadCommandManager)
        {
            _loadPanel = loadPanel;
            _loadCommandManager = loadCommandManager;

            _loadCommandManager.isOnLoad += ActivateOrDeactivatePanel;
        }

        private void ActivateOrDeactivatePanel(bool isActivate)
        {
            _loadPanel.SetActive(isActivate);
        }
    }
}