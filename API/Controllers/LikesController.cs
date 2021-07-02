using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entites;
using API.Extensions;
using API.Helper;
using API.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class LikesController:BaseApiController
    {
         private readonly IUserRepository _userRepository;
        private readonly ILikeRepository _likeRepository;

        public LikesController(IUserRepository userRepository,ILikeRepository likeRepository){
            _userRepository = userRepository;
            _likeRepository = likeRepository;
        }
        [HttpPost("{userName}")]
        public async Task<ActionResult> AddLike(string userName)
        {
            var sourceId = User.GetUserId();
          
            var likedUser=await _userRepository.GetUserByUsernameAsync(userName);
            var sourceUser=await _likeRepository.GetUserWithLikes(sourceId);
            

            if(likedUser== null) return NotFound();

            if(sourceUser.UserName==userName) return BadRequest("you cannot like userSelf");
        
             var userLike= await _likeRepository.GetUserLike(sourceId,likedUser.Id);

             if(userLike !=null) return BadRequest("you already likes this user");

             userLike=new UserLike{
                 SourceUserId=sourceId,
                 LikedUserId=likedUser.Id
             };
             sourceUser.LikedUsers.Add(userLike);

             if(await _userRepository.SaveAsyncAll()) return Ok();

             return BadRequest("Failed to Like User");
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikeDto>>> GetUserLike([FromQuery]LikesParams likesParams){
           likesParams.UserId=User.GetUserId();
            var user= await _likeRepository.GetUserLikes(likesParams);

            Response.AddPaginationHeader(user.CuurentPage,user.PageSize,user.TotalCount,user.TotalPages);
            return Ok(user);
        }
    
    }
}