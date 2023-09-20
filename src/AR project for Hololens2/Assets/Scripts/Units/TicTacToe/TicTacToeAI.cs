using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TicTacToe
{
	public static class TicTacToeAI
	{
		public static int FindBestMove(CellType[] gameBoard)
		{
			var bestMove = -1;
			var bestScore = int.MinValue;

			for (var i = 0; i < gameBoard.Length; i++)
			{
				if (gameBoard[i] == CellType.Empty)
				{
					gameBoard[i] = CellType.O;

					var score = MiniMax(gameBoard, 0, false);

					gameBoard[i] = CellType.Empty;

					if (score > bestScore)
					{
						bestScore = score;
						bestMove = i;
					}
				}
			}

			return bestMove;
		}

		private static int MiniMax(CellType[] board, int depth, bool isMaximizing)
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
						bestScore = Mathf.Max(bestScore, MiniMax(board, depth + 1, false));
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
						bestScore = Mathf.Min(bestScore, MiniMax(board, depth + 1, true));
						board[i] = CellType.Empty;
					}
				}

				return bestScore;
			}
		}

		private static int Evaluate(IReadOnlyList<CellType> board)
		{
			if (TicTacToeLogic.CheckForWin(board, CellType.O))
			{
				return 10;
			}

			if (TicTacToeLogic.CheckForWin(board, CellType.X))
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
				var rowTemp = row * 3;
				if (board[rowTemp] == CellType.O && board[rowTemp + 1] == CellType.O &&
				    board[rowTemp + 2] == CellType.Empty)
				{
					score += 1;
				}
				else if (board[rowTemp] == CellType.Empty &&
				         board[rowTemp + 1] == CellType.O &&
				         board[rowTemp + 2] == CellType.O)
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