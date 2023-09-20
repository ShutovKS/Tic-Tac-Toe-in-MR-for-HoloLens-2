using TicTacToe;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Manager.CoreLocale
{
	public class CoreLocaleManager : MonoBehaviour
	{
		[SerializeField] private GameObject _gameFieldPrefab;
		private GameObject _gameFieldGameObject;
		private GameFieldUI _gameFieldUI;
		private readonly CellType[] _gameBoard = new CellType[9];
		private bool _isXPlayerTurn = true;

		private void Start()
		{
			CreatedGameField();

			for (var i = 0; i < _gameBoard.Length; i++)
			{
				_gameBoard[i] = CellType.Empty;
			}

			if (!_isXPlayerTurn)
			{
				MakeAIMove();
			}
		}

		private void CreatedGameField()
		{
			_gameFieldGameObject = Instantiate(_gameFieldPrefab);

			_gameFieldUI = _gameFieldGameObject.GetComponent<GameFieldUI>();
			_gameFieldUI.RegisterCellButtonsCallback(OnClickCell);
			_gameFieldUI.RegisterGiveUpButtonsCallback(RestartGame);
			_gameFieldUI.RegisterExitButtonsCallback(ExitInMainMenu);
		}

		private void OnClickCell(int cellIndex)
		{
			if (_gameBoard[cellIndex] == CellType.Empty)
			{
				_gameBoard[cellIndex] = _isXPlayerTurn ? CellType.X : CellType.O;
				_gameFieldUI.SetImageInCell(
					cellIndex,
					_isXPlayerTurn
						? Resources.Load<Texture2D>("Icons/Cross")
						: Resources.Load<Texture2D>("Icons/Zero"));

				if (TicTacToeLogic.CheckForWin(_gameBoard, _isXPlayerTurn ? CellType.X : CellType.O))
				{
					Debug.Log((_isXPlayerTurn ? "X" : "O") + " победил!");
					RestartGame();
				}
				else if (TicTacToeLogic.IsBoardFull(_gameBoard))
				{
					Debug.Log("Ничья!");
					RestartGame();
				}
				else
				{
					_isXPlayerTurn = !_isXPlayerTurn;

					if (!_isXPlayerTurn)
					{
						MakeAIMove();
					}
				}
			}
		}

		private void MakeAIMove()
		{
			var bestMove = TicTacToeAI.FindBestMove(_gameBoard);
			OnClickCell(bestMove);
		}

		private void RestartGame()
		{
			SceneManager.LoadScene("3.CoreLocale");
		}

		private void ExitInMainMenu()
		{
			SceneManager.LoadScene("2.Meta");
		}
	}
}