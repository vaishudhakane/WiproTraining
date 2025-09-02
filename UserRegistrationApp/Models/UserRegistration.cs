   using System.ComponentModel.DataAnnotations;

   namespace UserRegistrationApp.Models
   {
       public class UserRegistration
       {
           [Required(ErrorMessage = "Username is required.")]
           public string Username { get; set; }

           [Required(ErrorMessage = "Email is required.")]
           [EmailAddress(ErrorMessage = "Invalid email format.")]
           public string Email { get; set; }

           [Required(ErrorMessage = "Password is required.")]
           [StringLength(100, ErrorMessage = "Password must be at least 6 characters long.", MinimumLength = 6)]
           public string Password { get; set; }

           [Required(ErrorMessage = "Please confirm your password.")]
           [Compare("Password", ErrorMessage = "Passwords do not match.")]
           public string ConfirmPassword { get; set; }
       }
   }
   