using MinimalAPIs.Models;

namespace MinimalAPIs.Services
{
    public interface ITokenService
    {
        string GetToken(string key, string issuer, string audience, UserModel user);
    }
}