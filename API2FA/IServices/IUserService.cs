using API2FA.Models;

namespace API2FA.IServices
{
    public interface IUserService
    {
        User GetById(Guid id);
    }
}
