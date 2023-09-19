using UnityEngine;
using UnityEngine.Events;

namespace UI
{
	public class GameFieldUI : MonoBehaviour
	{
		[SerializeField] private CellUI[] _cells;
		[SerializeField] private PlayfieldButtonsUI _playfieldButtons;

		public void RegisterCellButtonsCallback(UnityAction<int> action)
		{
			for (var i = 0; i < _cells.Length; i++)
			{
				var index = i;
				_cells[i].PressableButton.OnClicked.AddListener(() => { action?.Invoke(index); });
			}
		}

		public void SetImageInCell(int index, Texture2D texture2D)
		{
			var image = _cells[index].Image;
			image.material = null;
			image.color = Color.white;
			image.texture = texture2D;
		}

		public void RegisterGiveUpButtonsCallback(UnityAction action)
		{
			_playfieldButtons.GiveUpButton.OnClicked.AddListener(action);
		}
		
		public void RegisterExitButtonsCallback(UnityAction action)
		{
			_playfieldButtons.ExitButton.OnClicked.AddListener(action);
		}
	}
}