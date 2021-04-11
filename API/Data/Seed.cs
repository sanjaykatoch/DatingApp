using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entites;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    //This is class used for adding user if ther is no user in database these all are done by the json file.
    public class Seed
    {
        public static async Task SeedUsers(DataContext dbcontext)
        {
            if (await dbcontext.Users.AnyAsync()) return;

            var Data = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<Appuser>>(Data);
            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();
                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;
                dbcontext.Users.Add(user);
            }
            await dbcontext.SaveChangesAsync();
        }
    }
}