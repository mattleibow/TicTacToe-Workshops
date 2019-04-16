using Xamarin.Essentials;

namespace TicTacToe.UWP
{
	public sealed partial class MainPage
	{
		public MainPage()
		{
			InitializeComponent();

			Platform.MapServiceToken = "7BwF3VFU0pnkxSEUMOqt~1Tq_1fQUxzoCZtaSNGu9YA~AiDhuWbHbx3Lp9ie14MEFt-mRZyfiXrSngY7u60UzR3oBDuhoE_HkvHY0HQMAsOG";

			LoadApplication(new TicTacToe.App());
		}
	}
}
