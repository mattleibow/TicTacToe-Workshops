using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
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
			if (await RequestLocationPermissionAsync())
				_ = RequestLocationAsync();

			Analytics.TrackEvent("Navigate to game page");

			await Navigation.PushAsync(new GamePage());
		}

		private async Task<bool> RequestLocationPermissionAsync()
		{
			if (Preferences.ContainsKey(App.ShouldTrackLocationKey))
				return Preferences.Get(App.ShouldTrackLocationKey, false);

			var msg =
				"Do you want to join all your fellow gamers and share the city that you are playing in?" +
				Environment.NewLine + Environment.NewLine +
				"No personal information or location will be shared, just the current city you are in.";
			var shouldTrack = await DisplayAlert("Share your city?", msg, "Yes", "No");

			Preferences.Set(App.ShouldTrackLocationKey, shouldTrack);

			if (shouldTrack)
				Analytics.TrackEvent("Allowed location tracking");
			else
				Analytics.TrackEvent("Denied location tracking");

			return shouldTrack;
		}

		private async Task RequestLocationAsync()
		{
			// wait at least an hour before getting a new location
			var lastLocation = Preferences.Get(App.LastLocationTimestampKey, DateTime.MinValue);
			if (lastLocation + TimeSpan.FromHours(1) > DateTime.UtcNow)
				return;

			try
			{
				// get the location, first look at the cached location, then get something new
				var location =
					await Geolocation.GetLastKnownLocationAsync() ??
					await Geolocation.GetLocationAsync(new GeolocationRequest { DesiredAccuracy = GeolocationAccuracy.Lowest });
				if (location == null)
					return;

				// get the city from the GPS coordinates
				var placemarks = await Geocoding.GetPlacemarksAsync(location);
				var place = placemarks?.FirstOrDefault();

				// make sure we have a valid location
				if (string.IsNullOrWhiteSpace(place.Locality) || string.IsNullOrWhiteSpace(place.CountryName))
					return;

				// combine the city and country
				var city = $"{place.Locality}, {place.CountryName}";

				// make sure the city has changed
				if (Preferences.Get(App.LastLocationKey, null) == city)
					return;

				// save to the preferences
				Preferences.Set(App.LastLocationKey, city);
				Preferences.Set(App.LastLocationTimestampKey, DateTime.UtcNow);

				Analytics.TrackEvent("Updated city", new Dictionary<string, string>
				{
					{ "City", city }
				});
			}
			catch (FeatureNotEnabledException)
			{
				// the user disabled the GPS, so we will get the location the next time
			}
			catch (PermissionException)
			{
				// the user no longer wants to share location, so just skip
			}
			catch (Exception ex)
			{
				Crashes.TrackError(ex);
			}
		}
	}
}
