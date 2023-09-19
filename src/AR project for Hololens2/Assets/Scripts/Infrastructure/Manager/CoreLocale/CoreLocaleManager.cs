using System.Linq;
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
		private CellType[] _gameBoard = new CellType[9];
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

				if (CheckForWin(_isXPlayerTurn ? CellType.X : CellType.O))
				{
					Debug.Log((_isXPlayerTurn ? "X" : "O") + " победил!");
					RestartGame();
				}
				else if (IsBoardFull())
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
			var bestMove = FindBestMove();
			OnClickCell(bestMove);
		}

		private bool CheckForWin(CellType player)
		{
			for (var i = 0; i < 3; i++)
			{
				if (_gameBoard[i * 3] == player && _gameBoard[i * 3 + 1] == player && _gameBoard[i * 3 + 2] == player)
				{
					return true;
				}
			}

			for (var i = 0; i < 3; i++)
			{
				if (_gameBoard[i] == player && _gameBoard[i + 3] == player && _gameBoard[i + 6] == player)
				{
					return true;
				}
			}

			if (_gameBoard[0] == player && _gameBoard[4] == player && _gameBoard[8] == player)
			{
				return true;
			}

			if (_gameBoard[2] == player && _gameBoard[4] == player && _gameBoard[6] == player)
			{
				return true;
			}

			return false;
		}

		private bool IsBoardFull()
		{
			return _gameBoard.All(cell => cell != CellType.Empty);
		}

		private void RestartGame()
		{
			SceneManager.LoadScene("3.CoreLocale");
		}

		private void ExitInMainMenu()
		{
			SceneManager.LoadScene("2.Meta");
		}

		private int FindBestMove()
		{
			var bestMove = -1;
			var bestScore = int.MinValue;

			for (var i = 0; i < _gameBoard.Length; i++)
			{
				if (_gameBoard[i] == CellType.Empty)
				{
					_gameBoard[i] = CellType.O;

					var score = MiniMax(_gameBoard, 0, false);

					_gameBoard[i] = CellType.Empty;

					if (score > bestScore)
					{
						bestScore = score;
						bestMove = i;
					}
				}
			}

			return bestMove;
		}

		private int MiniMax(CellType[] board, int depth, bool isMaximizing)
		{
			var score = Evaluate(board);

			if (score == 10)
			{
				return score - depth;
			}

			if (score == -10)
			{
				return score + depth;
			}

			if (!board.Contains(CellType.Empty))
			{
				return 0;
			}

			if (isMaximizing)
			{
				var bestScore = int.MinValue;
				for (var i = 0; i < board.Length; i++)
				{
					if (board[i] == CellType.Empty)
					{
						board[i] = CellType.O;
						bestScore = Mathf.Max(bestScore, MiniMax(board, depth + 1, !isMaximizing));
						board[i] = CellType.Empty;
					}
				}

				return bestScore;
			}
			else
			{
				var bestScore = int.MaxValue;
				for (var i = 0; i < board.Length; i++)
				{
					if (board[i] == CellType.Empty)
					{
						board[i] = CellType.X;
						bestScore = Mathf.Min(bestScore, MiniMax(board, depth + 1, !isMaximizing));
						board[i] = CellType.Empty;
					}
				}

				return bestScore;
			}
		}

		private int Evaluate(CellType[] board)
		{
			if (CheckForWin(CellType.O))
			{
				return 10;
			}

			if (CheckForWin(CellType.X))
			{
				return -10;
			}

			if (!board.Contains(CellType.Empty))
			{
				return 0;
			}

			var score = 0;

			for (var row = 0; row < 3; row++)
			{
				if (board[row * 3] == CellType.O && board[row * 3 + 1] == CellType.O &&
				    board[row * 3 + 2] == CellType.Empty)
				{
					score += 1;
				}
				else if (board[row * 3] == CellType.Empty && board[row * 3 + 1] == CellType.O &&
				         board[row * 3 + 2] == CellType.O)
				{
					score += 1;
				}
			}

			for (var col = 0; col < 3; col++)
			{
				if (board[col] == CellType.O && board[col + 3] == CellType.O && board[col + 6] == CellType.Empty)
				{
					score += 1;
				}
				else if (board[col] == CellType.Empty && board[col + 3] == CellType.O && board[col + 6] == CellType.O)
				{
					score += 1;
				}
			}

			if (board[0] == CellType.O && board[4] == CellType.O && board[8] == CellType.Empty)
			{
				score += 1;
			}
			else if (board[0] == CellType.Empty && board[4] == CellType.O && board[8] == CellType.O)
			{
				score += 1;
			}

			if (board[2] == CellType.O && board[4] == CellType.O && board[6] == CellType.Empty)
			{
				score += 1;
			}
			else if (board[2] == CellType.Empty && board[4] == CellType.O && board[6] == CellType.O)
			{
				score += 1;
			}

			return score;
		}
	}
}