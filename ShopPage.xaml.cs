using CiurdareanPatriciaLab7.Models;
using Plugin.LocalNotification;
namespace CiurdareanPatriciaLab7;

public partial class ShopPage : ContentPage
{
	public ShopPage()
	{
		InitializeComponent();
	}
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;

        await App.Database.SaveShopAsync(shop);
        await Navigation.PopAsync();
    }
    async void OnShowMapButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        var address = shop.Adress;

        //var locations = await Geocoding.GetLocationsAsync(address);//android

        var options = new MapLaunchOptions
        {
            Name = "Magazinul meu\npreferat"
        };

        //var shoplocation = locations?.FirstOrDefault();//android
        
        var shoplocation = new Location(46.7492379, 23.5745597); 
        // pentru Windows Machine

        //var myLocation = await Geolocation.GetLocationAsync();//android
        
        var myLocation = new Location(46.7731796289, 23.6213886738);
        // pentru Windows Machine
        

        var distance = myLocation.CalculateDistance(
            shoplocation,
            DistanceUnits.Kilometers
        );

        if (distance < 5)
        {
            var request = new NotificationRequest
            {
                Title = "Ai de facut cumparaturi in apropiere!",
                Description = address,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(1)
                }
            };

            LocalNotificationCenter.Current.Show(request);
        }

        await Map.OpenAsync(shoplocation, options);
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;

        await App.Database.DeleteShopAsync(shop);
        await Navigation.PopAsync();
    }
}