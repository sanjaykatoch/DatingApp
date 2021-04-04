using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entites;
using API.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        public readonly DataContext _DataContext ;
        public UserRepository(DataContext dataContext)
        {
            _DataContext = dataContext;
        }

        public async Task<IEnumerable<Appuser>> GetUserAsync()
        {
           return  await _DataContext.Users
           .Include(p=>p.Photos)
           .ToListAsync();
        }

        public async Task<Appuser> GetUserByUserNameAsync(string userName)
        {
            return await _DataContext.Users
            .Include(p=>p.Photos)
            .FirstOrDefaultAsync(x=>x.UserName==userName);
        }

        public async Task<Appuser> GetUserIdByAsync(int id)
        {
           return await _DataContext.Users.FindAsync(id);
        }

        public async Task<bool> SaveAsyncAll()
        {
            return await _DataContext.SaveChangesAsync()>0;
        }

        public void Upate(Appuser appUser)
        {
            _DataContext.Entry(appUser).State=EntityState.Modified;
        }
    }
}