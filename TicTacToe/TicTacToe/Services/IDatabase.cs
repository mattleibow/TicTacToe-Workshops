using System.Threading.Tasks;

namespace TicTacToe
{
	public interface IDatabase
	{
		Task<bool> AddCompletedGameAsync(CompletedGame game);

		Task<int> GetGamePlayCountAsync(string board);
	}
}
