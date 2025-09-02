using OnlineBookStore.Models;


public interface IUserRepository
{
    Task<ApplicationUser?> FindByUserNameAsync(string userName);
    Task<ApplicationUser> CreateAsync(ApplicationUser user, string password);
    Task<bool> VerifyPasswordAsync(ApplicationUser user, string password);
}
