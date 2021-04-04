using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entites;

namespace API.Interface
{
    public interface IUserRepository
    {
         void Upate(Appuser appUser);

         Task<bool> SaveAsyncAll();

         Task<IEnumerable<Appuser>> GetUserAsync();

         Task<Appuser> GetUserIdByAsync(int id);

         Task<Appuser> GetUserByUserNameAsync(string userName);
    }
}