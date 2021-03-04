using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RobotLegs.Web.Models.Accounts
{
    public class ConfirmResetPasswordModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(32, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 32 characters long")]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Password and its confirmation do not match")]
        [Display(Name = "Confirm New Password")]
        public string ConfirmNewPassword { get; set; }

        [Required]
        [Display(Name = "Confirmation Code")]
        public string ConfirmResetPasswordCode { get; set; }
    }
}
