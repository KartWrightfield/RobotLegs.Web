using System.ComponentModel.DataAnnotations;

namespace RobotLegs.Web.Models.Accounts
{
    public class ConfirmModel
    {
        [Required]        
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Confirmation code is required")]
        [Display(Name = "Confirmation Code")]
        public string ConfirmationCode { get; set; }
    }
}
