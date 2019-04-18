using SQLite;

namespace TicTacToe
{
	public class GameStatistic
	{
		[PrimaryKey]
		public string Board { get; set; }

		public int PlayCount { get; set; }
	}
}
