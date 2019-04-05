using Microsoft.AppCenter.Analytics;
using System;
using Xamarin.Forms;

namespace TicTacToe
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

			scoresButton.IsEnabled = false;
		}

		private void OnPlayClicked(object sender, EventArgs e)
		{
			Analytics.TrackEvent("Navigate to game page");

			Navigation.PushAsync(new GamePage());
		}
	}
}
