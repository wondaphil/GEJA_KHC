namespace GEJA_KHC_MAUI.Views;

public partial class SettingPage : ContentPage
{
	public SettingPage()
	{
		InitializeComponent();
	}

    private async void btnServerAddressPage_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ServerAddressPage() { Title = "የሰርቨር አድራሻ" });
    }
}