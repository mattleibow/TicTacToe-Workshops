﻿using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using TicTacToe;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

[assembly: Dependency(typeof(HybridDatabase))]

namespace TicTacToe
{
	public partial class App : Application
	{
		public const string ShouldTrackLocationKey = "ShouldTrackLocation";
		public const string LastLocationKey = "LastLocation";
		public const string LastLocationTimestampKey = "LastLocationTimestamp";

		public App()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new MainPage());
		}

		protected override void OnStart()
		{
			// Handle when your app starts

#if DEBUG
			AppCenter.LogLevel = LogLevel.Verbose;
#endif

			AppCenter.Start(
				"android=5400d30b-a163-4ae8-ac99-8a886ba60e82;" +
				"ios=972a7d89-3b25-4d5f-88e5-302e0ba0d634;" +
				"uwp=a498e3f2-448c-4b7d-95e6-9352800dd652;",
				typeof(Analytics), typeof(Crashes));
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
