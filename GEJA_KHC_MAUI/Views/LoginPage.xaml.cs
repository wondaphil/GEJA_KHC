using GEJA_KHC_MAUI.ViewModels;

namespace GEJA_KHC_MAUI.Views;


[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class LoginPage : ContentPage
{
    public LoginViewModel vm = new LoginViewModel();

    public LoginPage()
    {
        this.BindingContext = vm;
        //this.BindingContext = new LoginViewModel(); 
        vm.DisplayPrompt += () => DisplayAlert("ጌጃ ቃ/ሕ ቤ/ክ", vm.LoginMessage, "እሺ");

        InitializeComponent();

        // Check if user has already logged in in previous usage of the app
        if (Preferences.ContainsKey("LoggedInUserName") && Preferences.ContainsKey("LoggedInPassword"))
        {
            vm.LoginStatus = true;
            Application.Current.MainPage = new AppShell();
            return;
        }

        entryUserName.Completed += (object sender, EventArgs e) =>
        {
            entryPassword.Focus();
        };

        entryPassword.Completed += (object sender, EventArgs e) =>
        {
            vm.LoginCommand.Execute(null);
        };
    }

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        try
        {
            if (vm.LoginStatus == true)
                Application.Current.MainPage = new AppShell();
        }
        catch (Exception err)
        {
            await DisplayAlert("ስህተት", err.Message, "እሺ");
        }
    }
}