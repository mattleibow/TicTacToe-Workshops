using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
			DisplayAlert("Play", "We are still working on this!", "OK");
		}
	}
}
