using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    // [ApiController]
    // [Route("api/[controller]")]
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Appuser>>> GetUsers()
        {
            var users= await _context.Users.ToListAsync();
            return users;
        }
        // [HttpGet]
        // public async Task<ActionResult<List<Appuser>>> GetAllUsers()
        // {
        //     List<Appuser> _appUsers=new List<Appuser>();
        //    // _appUsers=_context.Users.ToListAsync();
        //     return _appUsers;
        // }
        
        //api/user/3
        [Authorize]
        [HttpGet("id")]
        public async Task<ActionResult<Appuser>> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
            //_context.Users.Select(x=>x.Id==id);
           // return user;
        }
    }
}