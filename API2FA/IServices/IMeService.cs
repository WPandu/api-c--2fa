using API2FA.Models;
using API2FA.Requests;
using API2FA.Responses;

namespace API2FA.IServices
{
    public interface IMeService
    {
        User Me();
    }
}
