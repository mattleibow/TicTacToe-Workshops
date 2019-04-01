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

		protected override void OnAppearing()
		{
			base.OnAppearing();

			backButton.TranslateTo(0, 0, 500, Easing.BounceOut);
			currentPlayerLayout.TranslateTo(0, 0, 500, Easing.BounceOut);

			var minSize = Math.Min(Width, Height) * 0.75;
			boardView.Animate("resize", resize, length: 500, easing: Easing.SpringOut);
			void resize(double f)
			{
				var deltaSize = minSize * f;
				boardView.WidthRequest = deltaSize;
				boardView.HeightRequest = deltaSize;
			}
		}

		private void OnPageSizeChanged(object sender, EventArgs e)
		{
			var minSize = Math.Min(Width, Height) * 0.75;
			boardView.WidthRequest = minSize;
			boardView.HeightRequest = minSize;
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
				App.Current.MainPage = new MainPage();
			}
		}
	}
}
