using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace TutGeolocationApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private Location myLocation;

        private async void ButtonGetCurrentLoc_Clicked(object sender, EventArgs e)
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    labelLongLat.Text = "Lat: " + location.Latitude + " Long: " + location.Longitude;
                    myLocation = location;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }

        private async void ButtonConvert_Clicked(object sender, EventArgs e)
        {
            try
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(myLocation.Latitude, myLocation.Longitude);

                var placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    var geocodeAddress =
                        $"AdminArea:       {placemark.AdminArea}\n" +
                        $"CountryCode:     {placemark.CountryCode}\n" +
                        $"CountryName:     {placemark.CountryName}\n" +
                        $"FeatureName:     {placemark.FeatureName}\n" +
                        $"Locality:        {placemark.Locality}\n" +
                        $"PostalCode:      {placemark.PostalCode}\n" +
                        $"SubAdminArea:    {placemark.SubAdminArea}\n" +
                        $"SubLocality:     {placemark.SubLocality}\n" +
                        $"SubThoroughfare: {placemark.SubThoroughfare}\n" +
                        $"Thoroughfare:    {placemark.Thoroughfare}\n";

                    Console.WriteLine(geocodeAddress);
                    labelAddress.Text = geocodeAddress;
                }
            } catch (Exception exception)
            {

            }
        }

        private void ButtonCompareDistance_Clicked(object sender, EventArgs e)
        {
            Location sanFrancisco = new Location(37.783333, -122.416667);
            double miles = Location.CalculateDistance(myLocation, sanFrancisco, DistanceUnits.Miles);
            labelDistance.Text = miles + "Miles";
        }
    }
}
