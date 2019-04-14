using System;
using Xamarin.Forms;

namespace TicTacToe
{
	public partial class GamePage : ContentPage
	{
		public GamePage()
		{
			InitializeComponent();
		}

		protected override bool OnBackButtonPressed()
		{
			DoGoBack();

			// we did intercept the default action
			return true;
		}

		private void OnBackClicked(object sender, EventArgs e)
		{
			DoGoBack();
		}

		private async void DoGoBack()
		{
			var message = "Are you sure you want to leave the game? You might win!";
			if (await DisplayAlert("Back", message, "Yes", "No"))
			{
				await Navigation.PopAsync();
			}
		}
	}
}
