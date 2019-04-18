using Microsoft.AppCenter.Crashes;
using SQLite;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace TicTacToe
{
	public class LocalDatabase : IDatabase
	{
		private const string DatabaseName = "games.db3";

		private readonly Task initializedTask;

		private SQLiteAsyncConnection connection;

		public LocalDatabase()
		{
			initializedTask = Task.Run(async () =>
			{
				var databasePath = Path.Combine(FileSystem.AppDataDirectory, DatabaseName);
				connection = new SQLiteAsyncConnection(databasePath);

				await connection.CreateTableAsync<CompletedGame>();
				await connection.CreateTableAsync<GameStatistic>();
			});
		}

		public async Task<bool> AddCompletedGameAsync(CompletedGame game)
		{
			try
			{
				await initializedTask;

				// add the game to the table
				await connection.InsertAsync(game);

				// increase the stats for this board
				var stats = await connection.FindAsync<GameStatistic>(game.Board);
				stats = stats ?? new GameStatistic { Board = game.Board };
				stats.PlayCount++;
				await connection.InsertOrReplaceAsync(stats);

				return true;
			}
			catch (Exception ex)
			{
				Crashes.TrackError(ex);

				return false;
			}
		}

		public async Task<bool> RemoveCompletedGameAsync(string id)
		{
			try
			{
				await initializedTask;

				await connection.DeleteAsync<CompletedGame>(id);

				return true;
			}
			catch (Exception ex)
			{
				Crashes.TrackError(ex);

				return false;
			}
		}

		public async Task<CompletedGame[]> GetCompletedGamesAsync()
		{
			try
			{
				await initializedTask;

				return await connection.Table<CompletedGame>().ToArrayAsync();
			}
			catch (Exception ex)
			{
				Crashes.TrackError(ex);

				return new CompletedGame[0];
			}
		}

		public async Task<int> GetGamePlayCountAsync(string board)
		{
			try
			{
				await initializedTask;

				var result = await connection.FindAsync<GameStatistic>(board);

				return result?.PlayCount ?? 0;
			}
			catch (Exception ex)
			{
				Crashes.TrackError(ex);

				return -1;
			}
		}

		public async Task<bool> UpdateGameStatsAsync(GameStatistic stats)
		{
			try
			{
				await initializedTask;

				await connection.UpdateAsync(stats);

				return true;
			}
			catch (Exception ex)
			{
				Crashes.TrackError(ex);

				return false;
			}
		}
	}
}
