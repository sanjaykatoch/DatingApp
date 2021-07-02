using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entites;
using API.Helper;

namespace API.Interface
{
    public interface ILikeRepository
    {
        Task<UserLike> GetUserLike(int sourceId,int likedUserId);

        Task<Appuser> GetUserWithLikes(int UserId);

        Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams);
    }
}