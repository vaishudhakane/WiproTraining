using Microsoft.AspNetCore.Identity;

namespace Prep1.Data
{
    
    public class ApplicationUser  : IdentityUser 
    {
      
        public string? FullName { get; set; }
    }
}
