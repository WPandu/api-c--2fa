using API2FA.Helpers;
using API2FA.Models;
using API2FA.IServices;

namespace API2FA.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public User GetById(Guid id) 
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }
    }
}
