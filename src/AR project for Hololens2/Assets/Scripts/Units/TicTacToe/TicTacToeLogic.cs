using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
	public static class TicTacToeLogic
	{
		public static bool IsBoardFull(IEnumerable<CellType> gameBoard)
		{
			return gameBoard.All(cell => cell != CellType.Empty);
		}

		public static bool CheckForWin(IReadOnlyList<CellType> gameBoard, CellType player)
		{
			for (var i = 0; i < 3; i++)
			{
				if (gameBoard[i * 3] == player && gameBoard[i * 3 + 1] == player && gameBoard[i * 3 + 2] == player)
				{
					return true;
				}
			}

			for (var i = 0; i < 3; i++)
			{
				if (gameBoard[i] == player && gameBoard[i + 3] == player && gameBoard[i + 6] == player)
				{
					return true;
				}
			}

			if (gameBoard[0] == player && gameBoard[4] == player && gameBoard[8] == player)
			{
				return true;
			}

			if (gameBoard[2] == player && gameBoard[4] == player && gameBoard[6] == player)
			{
				return true;
			}

			return false;
		}
	}
}