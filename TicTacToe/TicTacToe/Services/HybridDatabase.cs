using Microsoft.AppCenter.Crashes;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace TicTacToe
{
	public class HybridDatabase
	{
		private readonly LocalDatabase localDatabase;
		private readonly CloudDatabase cloudDatabase;

		private bool isUploading;
		private bool isDownloading;

		public HybridDatabase()
		{
			localDatabase = new LocalDatabase();
			cloudDatabase = new CloudDatabase();

			Connectivity.ConnectivityChanged += OnConnectivityChanged;
		}

		public async Task<bool> AddCompletedGameAsync(CompletedGame game)
		{
			// cache the game locally
			if (!await localDatabase.AddCompletedGameAsync(game))
				return false;

			// make sure to upload all the cached games
			await UploadCachedGamesAsync();

			return true;
		}

		public async Task<int> GetGamePlayCountAsync(string board)
		{
			// try the cloud first
			var count = await DownloadGameCountAsync(board);

			// if there was a problem, use the local
			if (count == -1)
				count = await localDatabase.GetGamePlayCountAsync(board);

			return count;
		}

		public async Task UploadCachedGamesAsync()
		{
			// we are done if there is an upload in progress or no internet
			if (isUploading || Connectivity.NetworkAccess != NetworkAccess.Internet)
				return;

			isUploading = true;
			try
			{
				var localGames = await localDatabase.GetCompletedGamesAsync();
				foreach (var localGame in localGames)
				{
					// upload each game, ignore all errors
					await cloudDatabase.AddCompletedGameAsync(localGame);
					await localDatabase.RemoveCompletedGameAsync(localGame.Id);
				}
			}
			catch (Exception ex)
			{
				Crashes.TrackError(ex);
			}
			finally
			{
				isUploading = false;
			}
		}

		public async Task<int> DownloadGameCountAsync(string board)
		{
			// we are done if there is a download in progress or no internet
			if (isDownloading || Connectivity.NetworkAccess != NetworkAccess.Internet)
				return -1;

			isDownloading = true;
			try
			{
				// download the stats and update the local database
				var playCount = await cloudDatabase.GetGamePlayCountAsync(board);
				if (playCount != -1)
				{
					// ignore update failures
					await localDatabase.UpdateGameStatsAsync(new GameStatistic
					{
						Board = board,
						PlayCount = playCount
					});

					return playCount;
				}
			}
			catch (Exception ex)
			{
				Crashes.TrackError(ex);
			}
			finally
			{
				isDownloading = false;
			}

			return -1;
		}

		private async void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
		{
			// if the internet just came back, then upload the games
			if (e.NetworkAccess == NetworkAccess.Internet)
				await UploadCachedGamesAsync();
		}
	}
}
