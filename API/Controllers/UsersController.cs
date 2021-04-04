using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entites;
using API.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    // [ApiController]
    // [Route("api/[controller]")]
    [Authorize]
    public class UsersController : BaseApiController
    {
        //private readonly DataContext _context;
        public IUserRepository UserRepository ;
        public UsersController(IUserRepository userRepository)
        {
            UserRepository = userRepository;
            // _context = context;
        }
        [HttpGet]
        // [AllowAnonymous]
        public async Task<ActionResult<List<Appuser>>> GetUsers()
        {
            // var users = await _context.Users.ToListAsync();
            // return users;
            return Ok(await UserRepository.GetUserAsync());
        }
        // [HttpGet]
        // public async Task<ActionResult<List<Appuser>>> GetAllUsers()
        // {
        //     List<Appuser> _appUsers=new List<Appuser>();
        //    // _appUsers=_context.Users.ToListAsync();
        //     return _appUsers;
        // }

        //api/user/3
        // [Authorize]
      //  [HttpGet("id")]
        [HttpGet("{username}")]
        [Route("api/user/username")]
        public async Task<ActionResult<Appuser>> GetUser(string userName)
        {
            //return await _context.Users.FindAsync(id);

            return await UserRepository.GetUserByUserNameAsync(userName);
            //_context.Users.Select(x=>x.Id==id);
            // return user;
        }
    }
}