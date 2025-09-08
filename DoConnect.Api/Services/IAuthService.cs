using DoConnect.Api.Entities;

namespace DoConnect.Api.Services
{
    public interface IAuthService
    {
        Task<string> Register(User user, string password);
        Task<string?> Login(string username, string password);
    }
}
