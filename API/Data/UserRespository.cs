using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entites;
using API.Helper;
using API.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRespository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRespository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Appuser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<Appuser> GetUserByUsernameAsync(string userName)
        {
            return await _context.Users
            .Include(p => p.Photos)
            .FirstOrDefaultAsync(x => x.UserName == userName);
        }

        public async Task<IEnumerable<Appuser>> GetUsersAsync()
        {
            return await _context.Users
         .Include(p => p.Photos)
         .ToListAsync();
        }

        public async Task<bool> SaveAsyncAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Upate(Appuser appUser)
        {
            _context.Entry(appUser).State = EntityState.Modified;
        }

        Task<MemberDto> IUserRepository.GetMemberAsync(string userName)
        {
            return _context.Users.Where(x => x.UserName == userName)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider) // this is new automapper way to map the memberDTO with the 
            // .Select(user=>new MemberDto
                // {
                //     Id=user.Id,
                //     UserName=user.UserName
                // }
            .SingleOrDefaultAsync();
        }
        public async Task<PagedList<MemberDto>>  GetMembersAsync(UserParams userParams)
        {
            var query=_context.Users.AsQueryable();
            // ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            // .AsNoTracking()
            // .AsQueryable();
           
            query=query.Where(U=>U.UserName!=userParams.CurrentUsername);
            query=query.Where(x=>x.Gender==userParams.Gender);
            
            var MinAge=DateTime.Today.AddYears(-userParams.MinAge);
            var MaxAge=DateTime.Today.AddYears(-userParams.MaxAge-1);

            query=query.Where(x=>x.DateOfBirth>=MaxAge && x.DateOfBirth<=MinAge);

            query = userParams.OrderBy switch
            {
                "createdBy" => query.OrderByDescending(x => x.Created),
                _ => query.OrderByDescending(x => x.LastActive)
            } ;           
            // var minDOb=DateTime.Today.AddYears(-userParams.MaxAge-1);
            //  var maxDOb=DateTime.Today.AddYears(-userParams.MinAge);

           //  query=query.Where(x=>x.DateOfBirth>=minDOb && x.DateOfBirth<=maxDOb);
            // return await _context.Users.
            // ProjectTo<MemberDto>(_mapper.ConfigurationProvider).ToListAsync();

            return await PagedList<MemberDto>.CreateAsync(query.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).AsNoTracking()
            ,userParams.pageNumber,userParams.PageSize);
        }
    }
}