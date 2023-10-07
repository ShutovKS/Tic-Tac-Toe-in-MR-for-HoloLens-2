using UI.ServerSelection;
using UnityEngine;
using UnityEngine.Events;

namespace Infrastructure.Manager.CoreOnline
{
    public class ServerSelection
    {
        public ServerSelection(GameObject serverSelectionScreenPrefab)
        {
            _serverSelectionScreenPrefab = serverSelectionScreenPrefab;
        }

        private readonly GameObject _serverSelectionScreenPrefab;
        private ServerSelectionUI _serverSelectionUI;
        private GameObject _serverSelectionScreen;

        public void CreateServerSelectionScreen(Vector3 position, Quaternion rotation)
        {
            _serverSelectionScreen = Object.Instantiate(_serverSelectionScreenPrefab, position, rotation);
            _serverSelectionUI = _serverSelectionScreen.GetComponent<ServerSelectionUI>();
        }

        public void DestroyServerSelectionScreen()
        {
            Object.Destroy(_serverSelectionScreen);
        }

        public void RegisterHostButtonCallback(UnityAction callback) => _serverSelectionUI.RegisterHostButtonCallback(callback);
        public void RegisterClientButtonCallback(UnityAction callback) => _serverSelectionUI.RegisterClientButtonCallback(callback);
        public void RegisterExitButtonCallback(UnityAction callback) => _serverSelectionUI.RegisterExitButtonCallback(callback);
        
        public void SetActivePanel(bool isActive) => _serverSelectionUI.SetActivePanel(isActive);
    }
}
