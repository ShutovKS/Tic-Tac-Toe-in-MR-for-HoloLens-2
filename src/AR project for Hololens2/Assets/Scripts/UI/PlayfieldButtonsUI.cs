using MixedReality.Toolkit.UX;
using UnityEngine;

namespace UI
{
	public class PlayfieldButtonsUI : MonoBehaviour
	{
		[field: SerializeField] public PressableButton GiveUpButton { get; private set; }
		[field: SerializeField] public PressableButton ExitButton { get; private set; }
	}
}
