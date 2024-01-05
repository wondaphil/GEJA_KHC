using System.Globalization;

namespace GEJA_KHC_MAUI.ViewModels
{
    // A converter class which inverts a boolean value (switches it to its opposite value)
    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !((bool)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public static class WebApiURL
    {
        /// <summary>
        /// Web API address (e.g. http://192.168.43.17:778)
        /// </summary>
        public static string Address = Preferences.Get("HttpProtocol", "") + Preferences.Get("ServerAddress", "");
    }

    public static class ConnectivityViewModel
    {
        public static string StatusText = "";
        public static Color StatusColor = Colors.White;

        static ConnectivityViewModel()
        {
            UpdateStatus();
        }

        public static void UpdateStatus()
        {
            switch (Connectivity.NetworkAccess)
            {
                case NetworkAccess.Internet:
                    StatusText = "ኢንተርኔት አለ";
                    StatusColor = Colors.Green;
                    break;
                case NetworkAccess.Local:
                    StatusText = "የውስጥ ኔትወርክ ብቻ";
                    StatusColor = Colors.Orange;
                    break;
                case NetworkAccess.ConstrainedInternet:
                    StatusText = "የኢንተርኔት ግንኙነቱ ውስን ነው"; //such as behind a network login page
                    StatusColor = Colors.Yellow;
                    break;
                case NetworkAccess.None:
                    StatusText = "ኢንተርኔት የለም";
                    StatusColor = Colors.Red;
                    break;
                case NetworkAccess.Unknown:
                    StatusText = "የኢንተርኔት ሁኔታ አልታወቀም";
                    StatusColor = Colors.Yellow;
                    break;
                default:
                    break;
            }
        }
    }

    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Colors.Green : Colors.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class ServerAddressViewModel : GKHCBaseViewModel
    {
        private bool _EnableSetServerAddressButton;

        public ServerAddressViewModel()
        {
            _EnableSetServerAddressButton = false;
        }

        public bool EnableSetServerAddressButton
        {
            get { return _EnableSetServerAddressButton; }
            set
            {
                _EnableSetServerAddressButton = value;
                OnPropertyChanged(nameof(EnableSetServerAddressButton));
            }
        }

        public string ConnectivityStatusText
        {
            get { return ConnectivityViewModel.StatusText; }
            set
            {
                ConnectivityViewModel.UpdateStatus();

                OnPropertyChanged(nameof(ConnectivityStatusText));
                OnPropertyChanged(nameof(ConnectivityStatusColor));
            }
        }

        public Color ConnectivityStatusColor
        {
            get { return ConnectivityViewModel.StatusColor; }
        }
    }
}
