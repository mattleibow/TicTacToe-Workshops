using System.Windows.Input;
using Xamarin.Forms;

namespace TicTacToe
{
	public enum GameState
	{
		InProgress,
		GameOver
	}

	public enum Player
	{
		Nobody,
		X,
		O
	}

	public class GameViewModel : BindableObject
	{
		private Player currentPlayer;
		private Player winner;
		private GameState state;

		public GameViewModel()
		{
			MakeMoveCommand = new Command(OnMakeMove);
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

		private void OnMakeMove()
		{
			CurrentPlayer = CurrentPlayer == Player.X
				? Player.O
				: Player.X;
		}
	}
}
