using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entites;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    // [ApiController]
    [Authorize]
    // [Route("api/[controller]")]
    public class UsersController : BaseApiController
    {
        // private readonly DataContext _context;
        private readonly IUserRepository _userRepository;
        public readonly  IMapper _mapper ;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            // _context = context;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<MemberDto>>> GetUsers()
        {
            // var users = await _context.Users.ToListAsync();
            // return users;
            // var users = await _userRepository.GetUsersAsync();
            // var userToReturn=_mapper.Map<IEnumerable<MemberDto>>(users);
            // return Ok(userToReturn);
            var users = await _userRepository.GetMembersAsync();
            return Ok(users);
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
        [HttpGet("id")]
        public async Task<ActionResult<MemberDto>> GetUser(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return _mapper.Map<MemberDto>(user);
            //return Ok(user);
            // return await _context.Users.FindAsync(id);
            //_context.Users.Select(x=>x.Id==id);
            // return user;
        }
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            // var user= await _userRepository.GetUserByUsernameAsync(username);
            // return _mapper.Map<MemberDto>(user);
            return await _userRepository.GetMemberAsync(username);
        }
    }
}