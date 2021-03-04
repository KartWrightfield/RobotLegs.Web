using System.ComponentModel.DataAnnotations;

namespace RobotLegs.Web.Models.Accounts
{
    public class ResetPasswordModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
