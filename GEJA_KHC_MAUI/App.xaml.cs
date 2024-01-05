using GEJA_KHC_MAUI.ViewModels;
using Newtonsoft.Json;

namespace GEJA_KHC_MAUI;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        //MainPage = new AppShell();

        // If logged in user name and password data exist, go automatically to main page.
        // Otherwise, go to login page
        if (Preferences.ContainsKey("LoggedInUserName") && Preferences.ContainsKey("LoggedInPassword"))
            MainPage = new AppShell();
        else
            MainPage = new GEJA_KHC_MAUI.Views.LoginPage();

        if (Preferences.Get("FirstTimeRun", true))
        {
            Preferences.Set("HttpProtocol", "http://");
            Preferences.Set("ServerAddress", "192.168.167.73:778");
            Preferences.Set("FirstTimeRun", false);

        }
    }

    protected override void OnStart()
    {
        // Override OnStart

        // On startup, check if local users data is stored in local storage and save it if not
        StoreUserData();
    }

    protected override void OnSleep()
    {
    }

    protected override void OnResume()
    {
    }

    private void StoreUserData()
    {
        List<UserViewModel> userList = new List<UserViewModel>()
            {
                new UserViewModel() { UserName = "admin", Password = "admin", Role = "Admin" },
                new UserViewModel() { UserName = "user", Password = "user", Role = "User" },
            };

        if (!Preferences.ContainsKey("UserList"))
        {
            // Since preferences can store string value only, the list of user object has to be serialized into string
            // e.g. allusersstring = "[{\"UserName\":\"admin\",\"Password\":\"mypwd\",\"Role\":\"Admin\",\"LoggedIn\":false},...
            var serializedUserList = JsonConvert.SerializeObject(userList);

            Preferences.Set("UserList", serializedUserList);
        }
    }
}
