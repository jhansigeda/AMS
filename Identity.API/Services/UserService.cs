using Identity.API.Models;
using Identity.API.Services.IService;

namespace Identity.API.Services
{
    public class UserService : IUserService
    {
        private List<User> _users = new List<User>
        {
            new User { Username = "admin", Password = "admin123", Role = "Admin" },
            new User { Username = "customer", Password = "cust123", Role = "Customer" }
        };

        public User Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);
            if (user == null)
                return null;

            return user;
        }
    }
}
