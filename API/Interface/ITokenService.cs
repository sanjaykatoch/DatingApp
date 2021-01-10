using API.Entites;

namespace API.Interface
{
    public interface ITokenService
    {
         string CreateToekn(Appuser user);
    }
}