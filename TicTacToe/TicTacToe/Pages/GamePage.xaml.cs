using Microsoft.AppCenter.Analytics;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace TicTacToe
{
	public partial class GamePage : ContentPage
	{
		public GamePage()
		{
			InitializeComponent();
		}

		private GameViewModel ViewModel => BindingContext as GameViewModel;

		protected override void OnAppearing()
		{
			base.OnAppearing();

			// animate the HUD in
			backButton.TranslateTo(0, 0, 500, Easing.BounceOut);
			currentPlayerLayout.TranslateTo(0, 0, 500, Easing.BounceOut);

			// animate the board in
			var minSize = Math.Min(Width, Height) * 0.75;
			boardView.Animate("resize", resize, length: 500, easing: Easing.SpringOut);

			void resize(double f)
			{
				var deltaSize = minSize * f;
				boardView.WidthRequest = deltaSize;
				boardView.HeightRequest = deltaSize;
			}
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
			var inProgress = ViewModel.State == GameState.InProgress;

			var message = "Are you sure you want to leave the game? You might win!";
			if (!inProgress || await DisplayAlert("Back", message, "Yes", "No"))
			{
				var properties = new Dictionary<string, string>
				{
					{ "Game state", ViewModel.State.ToString() },
				};
				Analytics.TrackEvent("Go back to launcher page", properties);

				await Navigation.PopAsync();
			}
		}

		private void OnPageSizeChanged(object sender, EventArgs e)
		{
			var minSize = Math.Min(Width, Height) * 0.75;
			boardView.WidthRequest = minSize;
			boardView.HeightRequest = minSize;
		}
	}
}
