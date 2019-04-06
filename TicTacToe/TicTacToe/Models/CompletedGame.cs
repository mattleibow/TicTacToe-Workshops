using Newtonsoft.Json;
using System;
using System.Linq;

namespace TicTacToe
{
	public class CompletedGame
	{
		public CompletedGame()
		{
		}

		public CompletedGame(Player[] board, Player winner)
		{
			if (board?.Length != 9)
				throw new ArgumentException($"A game board must be exactly 9 elements. Recieved {board?.Length}.", nameof(board));

			Board = string.Concat(board.Select(GetPlayerRepresentation));
			Winner = GetPlayerRepresentation(winner);
		}

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("board")]
		public string Board { get; set; }

		[JsonProperty("winner")]
		public string Winner { get; set; }

		[JsonProperty("timestamp")]
		public DateTime Timestamp { get; set; }

		public static string GetPlayerRepresentation(Player player) =>
			player.HasFlag(Player.X) ? "X" :
			player.HasFlag(Player.O) ? "O" :
			"-";
	}
}
