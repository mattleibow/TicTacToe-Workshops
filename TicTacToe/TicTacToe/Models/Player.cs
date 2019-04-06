using System;

namespace TicTacToe
{
	[Flags]
	public enum Player
	{
		Nobody = 0,
		X = 1 << 0,
		O = 1 << 1,
		IsWinner = 1 << 2
	}
}
