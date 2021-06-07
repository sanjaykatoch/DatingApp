using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entites;
using API.Helper;

namespace API.Interface
{
    public interface IUserRepository
    {
         void Upate(Appuser appUser);

         Task<bool> SaveAsyncAll();

         Task<IEnumerable<Appuser>> GetUsersAsync();

         Task<Appuser> GetUserByIdAsync(int id);

         Task<Appuser> GetUserByUsernameAsync(string userName);

          Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
          Task<MemberDto> GetMemberAsync(string userName);
    }
}