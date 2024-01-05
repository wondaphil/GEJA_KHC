using System.ComponentModel;
using System.Windows.Input;
using Newtonsoft.Json;

using GEJA_KHC_MAUI.Models;
using GEJA_KHC_MAUI.ViewModels;
using System.Linq;
using System.IO;

namespace GEJA_KHC_MAUI.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public Action DisplayPrompt;
        private bool _LoginStatus;
        private string _LoginMessage;
        private Color _LoginStatusColor;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string username;
        private string password;
        //public ICommand SubmitCommand { protected set; get; }
        
        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            _LoginStatusColor = Colors.Red;
        }

        public string UserName
        {
            get { return username; }
            set
            {
                username = value;
                PropertyChanged(this, new PropertyChangedEventArgs("UserName"));
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }

        public bool LoginStatus
        {
            get { return _LoginStatus; }
            set
            {
                _LoginStatus = value;
                PropertyChanged(this, new PropertyChangedEventArgs("LoginStatus"));
            }
        }

        public Color LoginStatusColor
        {
            get { return _LoginStatusColor; }
            set
            {
                _LoginStatusColor = value;
                PropertyChanged(this, new PropertyChangedEventArgs("LoginStatusColor"));
            }
        }

        public string LoginMessage
        {
            get { return _LoginMessage; }
            set
            {
                _LoginMessage = value;
                PropertyChanged(this, new PropertyChangedEventArgs("LoginMessage"));
            }
        }

        private void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            //await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");

            // Since preferences are stored as  serialized string, the string has to be deserialized back to list of user object
            string allusersstring = Preferences.Get("UserList", "");
            var alluserslist = JsonConvert.DeserializeObject<List<UserViewModel>>(allusersstring); 
            UserViewModel user = alluserslist.Where(s => s.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            if (user != null)
            {
                if (user.Password == password) // username and password are correct
                {
                    _LoginStatus = true;
                    _LoginMessage = "ትክክለኛ መረጃ አስገብተዋል !! ይግቡ የሚለውን ይጫኑ";
                    _LoginStatusColor = Colors.Green;

                    // Save login info so that the user need not type user name and password every time using the app
                    Preferences.Set("LoggedInUserName", user.UserName);
                    Preferences.Set("LoggedInPassword", user.Password);
                    
                    //await Application.Current.SavePropertiesAsync();

                    // Save user role to allow or disallow features (some features are allowed only for admin)
                    Preferences.Set("LoggedInUserRole", user.Role);
                }
                else // username is correct but password is not correct
                {
                    _LoginStatus = false;
                    _LoginMessage = "ያስገቡት የይለፍ ቃል ትክክል አይደለም! እባክዎን እንደገና ይሞክሩ።";
                    _LoginStatusColor = Colors.Red;
                    //DisplayPrompt();
                }
            }
            else // if both username and password are NOT correct
            {
                //DisplayInvalidLoginPrompt();
                _LoginStatus = false;
                _LoginMessage = "የተጠቃሚ ስም ወይም የይለፍ ቃል ተሳስተዋልያ! እባክዎን እንደገና ይሞክሩ።";
                _LoginStatusColor = Colors.Red;
                //DisplayPrompt();
            }
            PropertyChanged(this, new PropertyChangedEventArgs("LoginMessage"));
            PropertyChanged(this, new PropertyChangedEventArgs("LoginStatusColor"));
        }
    }
}
