using System;
using System.Linq;

namespace TicTacToe
{
	public class CompletedGame
	{
		public string Id { get; set; }

		public string Board { get; set; }

		public string Winner { get; set; }

		public DateTimeOffset Timestamp { get; set; }

		public static CompletedGame Create(Player[] board, Player winner) =>
			new CompletedGame
			{
				Id = Guid.NewGuid().ToString(),
				Board = string.Concat(board.Select(GetPlayerRepresentation)),
				Winner = GetPlayerRepresentation(winner),
				Timestamp = DateTimeOffset.UtcNow,
			};

		private static string GetPlayerRepresentation(Player player) =>
			player.HasFlag(Player.X) ? "X" :
			player.HasFlag(Player.O) ? "O" :
			"-";
	}
}
