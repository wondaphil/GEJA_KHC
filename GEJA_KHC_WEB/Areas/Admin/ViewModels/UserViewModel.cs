using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using GEJA_KHC_WEB.Models;
using System.ComponentModel.DataAnnotations;

namespace GEJA_KHC_WEB.Areas.Admin.ViewModels
{
    public class UserRoleViewModel
    {
        public AspNetUsers User { get; set; }
        public AspNetRoles Role { get; set; }
    }

    public class RegisterUserViewModel
    {
        //I added this...
        [Required]
        [Display(Name = "የተጠቃሚ ደረጃዎች")]
        public string UserRoles { get; set; }

        //I added this...
        [Required]
        [Display(Name = "የተጠቃሚ ስም")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "ኢሜይል")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "የማለፊያ ቃሉ ርዝመት {2} ፊደላት መሆን አለበት።", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "ማለፊያ ቃል")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ማለፊያ ቃል ማረጋገጫ")]
        [Compare("Password", ErrorMessage = "ያስገቡት ማለፊያ ቃልና ማረጋገጫው አንድ ዓይነት አይደሉም።")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangeUserPasswordViewModel
    {
        public string UserId;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "የቀድሞው ማለፊያ ቃል")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "የማለፊያ ቃሉ ርዝመት {2} ፊደላት መሆን አለበት።", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "አዲስ ማለፊያ ቃል")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "አዲስ ማለፊያ ቃል ማረጋገጫ።")]
        [Compare("NewPassword", ErrorMessage = "የአዲሱ ማለፊያ ቃልና ማረጋገጫው አንድ ዓይነት አይደሉም።")]
        public string ConfirmPassword { get; set; }
    }
}