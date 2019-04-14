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

		private async void OnPlayClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new GamePage());
		}
	}
}
