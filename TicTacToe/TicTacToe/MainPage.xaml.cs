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
			App.Current.MainPage = new GamePage();
		}
	}
}
