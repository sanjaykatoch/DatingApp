using System;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.Text.Json;

using API.Entites;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext dbcontext){

            if(await dbcontext.Users.AnyAsync()) return;

            var Data=await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users=JsonSerializer.Deserialize<List<Appuser>>(Data);
            foreach (var user in users)
            {
                using  var hmac=new HMACSHA512();
                user.UserName=user.UserName;
                user.PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt=hmac.Key;
                dbcontext.Users.Add(user);
            }
            await dbcontext.SaveChangesAsync();
        }
       
    }
}