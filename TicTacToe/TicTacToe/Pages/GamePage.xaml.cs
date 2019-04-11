using Microsoft.AppCenter.Analytics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

		private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(GameViewModel.State))
			{
				// when the game is over, show the endgame popup
				if (ViewModel.State == GameState.GameOver)
				{
					endgamePopup.IsVisible = true;
					endgamePopupBackground.FadeTo(0.95);
					endgamePopupContents.ScaleTo(1);
				}
			}
			else if (e.PropertyName == nameof(GameViewModel.BoardPlayCount))
			{
				// when the stats are loaded, show the stats
				endgamePopupStats.FadeTo(1);
			}
		}

		private async void OnPlayAgainClicked(object sender, EventArgs e)
		{
			Analytics.TrackEvent("Play again");

			// first, fade out the entire popup
			await endgamePopup.FadeTo(0);

			// then, reset all the animations to their initial values
			endgamePopup.IsVisible = false;
			endgamePopup.Opacity = 1;
			endgamePopupBackground.Opacity = 0;
			endgamePopupContents.Scale = 0;
			endgamePopupStats.Opacity = 0;

			// finally, create a clean view model
			BindingContext = new GameViewModel();
			ViewModel.PropertyChanged += OnViewModelPropertyChanged;
		}
	}
}
