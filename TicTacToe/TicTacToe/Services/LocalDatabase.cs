using Microsoft.AppCenter.Crashes;
using SQLite;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace TicTacToe
{
	public class LocalDatabase
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
			});
		}

		public async Task<bool> AddCompletedGameAsync(CompletedGame game)
		{
			try
			{
				await initializedTask;

				await connection.InsertAsync(game);

				return true;
			}
			catch (Exception ex)
			{
				Crashes.TrackError(ex);

				return false;
			}
		}

		public async Task<int> GetGamePlayCountAsync(string board)
		{
			try
			{
				await initializedTask;

				var result = await connection.Table<CompletedGame>()
					.Where(g => g.Board == board)
					.CountAsync();

				return result;
			}
			catch (Exception ex)
			{
				Crashes.TrackError(ex);

				return -1;
			}
		}
	}
}
