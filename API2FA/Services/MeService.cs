using API2FA.Helpers;
using API2FA.Models;
using API2FA.Requests;
using API2FA.Responses;
using API2FA.IServices;

namespace API2FA.Services
{
    public class MeService : IMeService
    {
        private DataContext _context;
        private HttpContext _httpContext;

        public MeService(DataContext context, HttpContext httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public User Me()
        {
            IUserService userService = new UserService(_context);
            return userService.GetById(_httpContext.Items["UserID"].ToString());
        }
    }
}
