using System;
using System.Globalization;
using Xamarin.Forms;

namespace TicTacToe
{
	public class ColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var player = (Player)value;

			if (player.HasFlag(Player.IsWinner))
				return App.Current.Resources["DefaultRed"];

			return App.Current.Resources["AlmostBlack"];
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
