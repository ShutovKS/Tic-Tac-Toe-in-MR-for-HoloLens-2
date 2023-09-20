using UnityEngine;
using UnityEngine.Events;

namespace UI.MainMenu
{
	public class MainMenuUI : MonoBehaviour
	{
		[SerializeField] private MainButtonsUI _mainButtonsUI;
		
		public void RegisterOnlineGameButtonCallback(UnityAction callback)
		{
			_mainButtonsUI.OnlineGameButton.OnClicked.AddListener(callback);
		}
		
		public void RegisterOfflineGameButtonCallback(UnityAction callback)
		{
			_mainButtonsUI.OfflineGameButton.OnClicked.AddListener(callback);
		}
		
		public void RegisterExitButtonCallback(UnityAction callback)
		{
			_mainButtonsUI.ExitButton.OnClicked.AddListener(callback);
		}
	}
}