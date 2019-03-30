namespace TicTacToe.UWP
{
	public sealed partial class MainPage
	{
		public MainPage()
		{
			InitializeComponent();

			LoadApplication(new TicTacToe.App());
		}
	}
}
