using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace TicTacToe
{
	public class GameViewModel : BindableObject
	{
		private Player currentPlayer;
		private Player winner;
		private GameState state;

		public GameViewModel()
		{
			MakeMoveCommand = new Command<string>(OnMakeMove);
			Board = new Player[9];
			CurrentPlayer = Player.X;
		}

		public ICommand MakeMoveCommand { get; }

		public Player[] Board { get; }

		public Player CurrentPlayer
		{
			get { return currentPlayer; }
			set
			{
				currentPlayer = value;
				OnPropertyChanged();
			}
		}

		public Player Winner
		{
			get { return winner; }
			set
			{
				winner = value;
				OnPropertyChanged();
			}
		}

		public GameState State
		{
			get { return state; }
			set
			{
				state = value;
				OnPropertyChanged();
			}
		}

		private void OnMakeMove(string indexString)
		{
			if (State == GameState.GameOver)
				return;

			if (!int.TryParse(indexString, out var index))
				return;
			if (index < 0 || index >= Board.Length)
				return;

			if (Board[index] != Player.Nobody)
				return;

			Board[index] = CurrentPlayer;
			OnPropertyChanged(nameof(Board));

			var possible = DetectGameOver();
			if (possible != null)
			{
				// there was a winner or a tie
				Winner = possible.Value.Winner;
				State = GameState.GameOver;
				CurrentPlayer = Player.Nobody;

				// add the result to the board
				foreach (var pos in possible.Value.Positions)
					Board[pos] |= Player.IsWinner;
				OnPropertyChanged(nameof(Board));
			}
			else
			{
				// we are still going
				State = GameState.InProgress;
				CurrentPlayer = CurrentPlayer == Player.X
					? Player.O
					: Player.X;
			}
		}

		private (Player Winner, int[] Positions)? DetectGameOver()
		{
			// check vertical for winner
			for (var i = 0; i < 3; i++)
			{
				var vertWin = CheckLine(i, i + 3, i + 6);
				if (vertWin.Winner != Player.Nobody)
					return vertWin;
			}

			// check horizontal for winner
			for (var i = 0; i < 9; i += 3)
			{
				var horizWin = CheckLine(i, i + 1, i + 2);
				if (horizWin.Winner != Player.Nobody)
					return horizWin;
			}

			// check top-left to bottom-right for winner
			var diag1Win = CheckLine(0, 4, 8);
			if (diag1Win.Winner != Player.Nobody)
				return diag1Win;

			// check top-right to bottom-left for winner
			var diag2Win = CheckLine(2, 4, 6);
			if (diag2Win.Winner != Player.Nobody)
				return diag2Win;

			// check tie
			if (Board.All(b => b != Player.Nobody))
				return (Player.Nobody, new int[0]);

			return null;

			(Player Winner, int[] Positions) CheckLine(int a, int b, int c)
			{
				if (Board[a] == Board[b] && Board[a] == Board[c])
					return (Board[a], new[] { a, b, c });

				return (Player.Nobody, new int[0]);
			}
		}
	}
}
