using MixedReality.Toolkit.UX;
using UnityEngine;
using UnityEngine.Events;

namespace UI.ServerSelection
{
    public class ServerSelectionUI : MonoBehaviour
    {
        [SerializeField] private PressableButton onHostButton;
        [SerializeField] private PressableButton onClientButton;
        [SerializeField] private PressableButton onExitButton;
        [SerializeField] private GameObject serverSelectionPanel;
        
        public void RegisterHostButtonCallback(UnityAction callback) => onHostButton.OnClicked.AddListener(callback);
        public void RegisterClientButtonCallback(UnityAction callback) => onClientButton.OnClicked.AddListener(callback);
        public void RegisterExitButtonCallback(UnityAction callback) => onExitButton.OnClicked.AddListener(callback);
        
        public void UnregisterHostButtonCallback(UnityAction callback) => onHostButton.OnClicked.RemoveListener(callback);
        public void UnregisterClientButtonCallback(UnityAction callback) => onClientButton.OnClicked.RemoveListener(callback);
        public void UnregisterExitButtonCallback(UnityAction callback) => onExitButton.OnClicked.RemoveListener(callback);
        
        public void SetActivePanel(bool isActive) => serverSelectionPanel.SetActive(isActive);
    }
}
