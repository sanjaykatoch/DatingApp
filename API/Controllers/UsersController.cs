using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entites;
using API.Extensions;
using API.Helper;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public readonly IPhotoService _photoService;
        public UsersController(IUserRepository userRepository, IMapper mapper,IPhotoService photoService)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _photoService=photoService;
            // _context = context;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers([FromQuery]UserParams userParams)
        {
            var user=await _userRepository.GetUserByUsernameAsync(User.GetUsername());
            if (user != null)
            {
                userParams.CurrentUsername = user.UserName;
                if (string.IsNullOrEmpty(userParams.Gender))
                {
                    userParams.Gender = user.Gender == "male" ? "female" : "male";
                }
            }

            

            var users = await _userRepository.GetMembersAsync(userParams);
            Response.AddPaginationHeader(users.CuurentPage,users.PageSize,
            users.TotalCount,users.TotalPages);

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
        [HttpGet("{username}",Name="GetUser")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            // var user= await _userRepository.GetUserByUsernameAsync(username);
            // return _mapper.Map<MemberDto>(user);
            return await _userRepository.GetMemberAsync(username);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto member)
        {
            // var username=User.GetUsername();

            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            _mapper.Map(member, user);

            _userRepository.Upate(user);

            if (await _userRepository.SaveAsyncAll()) return NoContent();
            return BadRequest("Fail To Update");

        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var user=await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            var result=await _photoService.AddPhotoAysnc(file);

            if(result.Error!=null) return BadRequest(result.Error.Message);

            var photo=new Photo{
                Url=result.SecureUrl.AbsoluteUri,
                PublicId=result.PublicId
            };

            if(user.Photos.Count==0){
                photo.IsMain=true;
            }
            user.Photos.Add(photo);

            if(await _userRepository.SaveAsyncAll()){
                //return CreatedAtRoute("GetUser",_mapper.Map<PhotoDto>(photo));
                return CreatedAtRoute("GetUser",new{username=user.UserName},_mapper.Map<PhotoDto>(photo));
                //return _mapper.Map<PhotoDto>(photo);
            }
            return BadRequest("Problem While Adding Photo");
        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            var user=await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            var photo=user.Photos.FirstOrDefault(x=>x.Id==photoId);

            if(photo.IsMain) return BadRequest("This photo is already your main photo");

            var currentMain=user.Photos.FirstOrDefault(x=>x.IsMain);

            if(currentMain!=null) currentMain.IsMain=false;
            photo.IsMain=true;
            
            if(await _userRepository.SaveAsyncAll()) return NoContent();

            return BadRequest("Failed to Set Main Photo");
        }
    [HttpDelete("delete-photo/{photoId}")]
    public async Task<ActionResult> DeletePhoto(int photoId){
        var user=await _userRepository.GetUserByUsernameAsync(User.GetUsername());

        var photo=user.Photos.FirstOrDefault(x=>x.Id==photoId);

        if(photo==null) return NotFound("Photo is Not Found");

        if(photo.IsMain) return BadRequest("you cannot Delete Main Photo");

        if(photo.PublicId!=null){

           var result= await _photoService.DeletePhotoAsync(photo.PublicId);
           if(result.Error!=null) return BadRequest(result.Error.Message);
        }
        user.Photos.Remove(photo);
        if(await _userRepository.SaveAsyncAll()) return Ok();

        return BadRequest("Failed to remove photo");
    }

    }
}