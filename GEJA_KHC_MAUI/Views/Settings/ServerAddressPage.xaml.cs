using GEJA_KHC_MAUI.ViewModels;

namespace GEJA_KHC_MAUI.Views;

public partial class ServerAddressPage : ContentPage
{
    ServerAddressViewModel viewModel = new ServerAddressViewModel();

    public ServerAddressPage()
	{
		InitializeComponent();

        this.BindingContext = viewModel = new ServerAddressViewModel();
        btnSetServerAddress.SetBinding(IsEnabledProperty, "EnableSetServerAddressButton");
        frmConnectivityStatus.SetBinding(BackgroundColorProperty, "ConnectivityStatusColor");
        lblConnectivityStatus.SetBinding(Label.TextProperty, "ConnectivityStatusText");

        if (Preferences.ContainsKey("HttpProtocol") && Preferences.ContainsKey("ServerAddress"))
        {
            ddlProtocolType.SelectedItem = Preferences.Get("HttpProtocol", "http://");
            entryAddress.Text = Preferences.Get("ServerAddress", "");
            viewModel.EnableSetServerAddressButton = false;
        }
    }

    private void btnSetServerAddress_Clicked(object sender, EventArgs e)
    {
        Preferences.Set("HttpProtocol", ddlProtocolType.SelectedItem.ToString());
        Preferences.Set("ServerAddress", entryAddress.Text);
        WebApiURL.Address = Preferences.Get("HttpProtocol", "") + Preferences.Get("ServerAddress", "");
        viewModel.EnableSetServerAddressButton = false;
    }

    private void OnAddressChange(object sender, EventArgs e)
    {
        viewModel.EnableSetServerAddressButton = true;
    }
}