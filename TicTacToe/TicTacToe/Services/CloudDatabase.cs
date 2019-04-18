using Microsoft.AppCenter.Crashes;
using Microsoft.Azure.Documents.Client;
using System;
using System.Threading.Tasks;

namespace TicTacToe
{
	public class CloudDatabase
	{
		private const string EndpointUri = "https://tictactoe-workshops.documents.azure.com:443/";
		private const string AuthKey = "miczHwBiecMWcNmM5XijfRiJcKdH3aoAQyLCI15d8Bi0042By7jrtPiPaQCsa0pTRP3i35HCj71eqLdiBRpfWg==";

		private const string DatabaseName = "games";
		private const string CollectionName = "completed-games";

		private readonly Task initializedTask;

		private DocumentClient client;
		private Uri collection;

		public CloudDatabase()
		{
			initializedTask = Task.Run(() =>
			{
				client = new DocumentClient(new Uri(EndpointUri), AuthKey);
				collection = UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName);
			});
		}

		public async Task<bool> AddCompletedGameAsync(CompletedGame game)
		{
			try
			{
				await initializedTask;

				await client.CreateDocumentAsync(collection, game);

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
