using MixedReality.Toolkit.UX;
using UnityEngine;

namespace UI.MainMenu
{
	public class MainButtonsUI : MonoBehaviour
	{
		[field: SerializeField] public PressableButton OnlineGameButton { get; private set; }
		[field: SerializeField] public PressableButton OfflineGameButton { get; private set; }
		[field: SerializeField] public PressableButton ExitButton { get; private set; }
	}
}