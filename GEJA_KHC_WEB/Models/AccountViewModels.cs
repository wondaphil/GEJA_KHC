using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GEJA_KHC_WEB.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "ኮድ")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "ይህንን የዌብ መጠቀሚያ ላስታውሰው?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "ኢሜይል")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        //I added this...
        [Required]
        [Display(Name = "የተጠቃሚ ስም")]
        public string UserName { get; set; }

        //[Required]
        //[Display(Name = "Email")]
        //[EmailAddress]
        //public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "ማለፊያ ቃል")]
        public string Password { get; set; }

        [Display(Name = "አስታውሰኝ?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        //I added this...
        [Required]
        [Display(Name = "የተጠቃሚ ደረጃ")]
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
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "ያስገቡት ማለፊያ ቃልና ማረጋገጫው አንድ ዓይነት አይደሉም።")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "የማለፊያ ቃሉ ርዝመት {2} ፊደላት መሆን አለበት.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "ማለፊያ ቃል")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ማለፊያ ቃል ማረጋገጫ")]
        [Compare("Password", ErrorMessage = "ያስገቡት ማለፊያ ቃልና ማረጋገጫው አንድ ዓይነት አይደሉም.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "ኢሜይል")]
        public string Email { get; set; }
    }
}
