using GEJA_KHC_MAUI.Views;

namespace GEJA_KHC_MAUI.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        var action = await DisplayAlert("ጌጃ ቃ/ሕ ቤ/ክ", "በእርግጥ ለመውጣት ፈልገሃል?", "አዎን", "አይ");

        if (action)
        {
            // On logout, remove saved user name and password
            Preferences.Remove("LoggedInUserName");
            Preferences.Remove("LoggedInPassword");
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }

    private async void OnExitClicked(object sender, EventArgs e)
    {
        var action = await DisplayAlert("ጌጃ ቃ/ሕ ቤ/ክ", "በእርግጥ ፕሮግራሙን ለመዝጋት ፈልገሃል?", "አዎን", "አይ");

        if (action)
            System.Diagnostics.Process.GetCurrentProcess().Kill();
    }

    private async void OnAboutClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AboutPage());
    }

    private async void OnHelpClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HelpPage());
    }
}

