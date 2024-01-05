using System;
using System.Collections.Generic;
using System.Text;

namespace GEJA_KHC_MAUI.ViewModels
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool LoggedIn { get; set; }
    }
}
