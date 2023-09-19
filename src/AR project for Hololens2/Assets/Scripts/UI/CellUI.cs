using MixedReality.Toolkit.UX;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class CellUI : MonoBehaviour
	{
		[field: SerializeField] public RawImage Image { get; private set; }
		[field: SerializeField] public PressableButton PressableButton { get; private set; }
	}
}
