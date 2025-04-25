using Identity.API.Models;

namespace Identity.API.Services.IService
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
    }
}
