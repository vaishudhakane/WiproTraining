using System.ComponentModel.DataAnnotations;

namespace Core_MVC_demo2.Models
{
    public class UserView
    {
        [Required(ErrorMessage = "User ID is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }   

    }
}
