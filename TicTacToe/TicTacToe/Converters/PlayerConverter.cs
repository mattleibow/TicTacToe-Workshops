﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace TicTacToe
{
	public class PlayerConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var player = (Player)value;

			if (player == Player.X)
				return "X";

			if (player == Player.O)
				return "O";

			return string.Empty;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
