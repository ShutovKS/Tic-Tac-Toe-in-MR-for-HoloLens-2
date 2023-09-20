using UI.MainMenu;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Manager.Meta
{
	public class MetaManager : MonoBehaviour
	{
		[SerializeField] private MainMenuUI _mainMenuUI;

		private void Start()
		{
			_mainMenuUI.RegisterOnlineGameButtonCallback(StartOnlineGame);
			_mainMenuUI.RegisterOfflineGameButtonCallback(StartOfflineGame);
			_mainMenuUI.RegisterExitButtonCallback(ExitGame);
		}

		private void StartOnlineGame()
		{
			SceneManager.LoadScene("3.CoreOnline");
		}

		private void StartOfflineGame()
		{
			SceneManager.LoadScene("3.CoreLocale");
		}

		private void ExitGame()
		{
			Application.Quit();
		}
	}
}