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
        private TokenManager _tokenManager;

        public MeService(DataContext context, TokenManager tokenManager)
        {
            _context = context;
            _tokenManager = tokenManager;
        }

        public User Me()
        {
            return null;
        }
    }
}
