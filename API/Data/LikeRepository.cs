using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entites;
using API.Extensions;
using API.Helper;
using API.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class LikeRepository : ILikeRepository
    {
        private readonly DataContext _context;

        public LikeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<UserLike> GetUserLike(int sourceId, int likedUserId)
        {
            return await _context.Likes.FindAsync(sourceId, likedUserId);
        }
        public async Task<Appuser> GetUserWithLikes(int UserId)
        {
            return await _context.Users
            .Include(x => x.LikedUsers)
            .FirstOrDefaultAsync(x => x.Id == UserId);
        }
        public async Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams)
        {
            var users = _context.Users.OrderBy(users => users.UserName).AsQueryable();
            var likes = _context.Likes.AsQueryable();
            //throw new System.NotImplementedException();
            if (likesParams.Predicate == "liked")
            {
                likes = likes.Where(x => x.SourceUserId == likesParams.UserId);
                users = likes.Select(x => x.LikedUser);
            }
            if (likesParams.Predicate == "likedBy")
            {
                likes = likes.Where(x => x.LikedUserId == likesParams.UserId);
                users = likes.Select(x => x.SourceUser);
            }
            var likedUser=users.Select(user => new LikeDto
            {
                Username = user.UserName,
                KnownsAs = user.KnownAs,
                Age = user.DateOfBirth.CalculateAge(),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain).Url,
                City = user.City,
                Id = user.Id
            });

            return await PagedList<LikeDto>.CreateAsync(likedUser,likesParams.pageNumber,likesParams.PageSize);
        }


    }
}