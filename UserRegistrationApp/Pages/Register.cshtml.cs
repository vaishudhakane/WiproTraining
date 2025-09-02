   using Microsoft.AspNetCore.Mvc;
   using Microsoft.AspNetCore.Mvc.RazorPages;
   using UserRegistrationApp.Models;

   namespace UserRegistrationApp.Pages
   {
       public class RegisterModel : PageModel
       {
           [BindProperty]
           public UserRegistration User { get; set; }

           public void OnGet()
           {
               User = new UserRegistration();
           }

           public IActionResult OnPost()
           {
               if (!ModelState.IsValid)
               {
                   return Page();
               }

             
               return RedirectToPage("Success"); // Redirect to a success page or display a message
           }
       }
   }
   