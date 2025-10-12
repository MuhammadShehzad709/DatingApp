using DatingApi.ApiHelpers;
using DatingApi.Data;
using DatingApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace DatingApi.Repositories.MemberRepositories
{
    public class MemberRepository(AppDbContext context) : IMemberRepository
    {
        public async Task<PaginationReult<Member>> GetAsyMembersAsync(PagingParams pagingParams)
        {
            var query=context.Members.AsQueryable();
            return await PaginationHelper.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public async Task<Member?> GetMemberByIdAsync(Guid Id)
        {
            return await context.Members.FindAsync(Id);
        }

        public async Task<Member?> GetMemberForUpdate(Guid Id)
        {
            return await context.Members.Include(x=>x.User).Include(x=>x.photos).SingleOrDefaultAsync(x=>x.Id==Id);
        }

        public async Task<List<Photo>> GetPhotosForMemberAsync(Guid memberId)
        {
            return await context.Members.Where(x=>x.Id == memberId).SelectMany(x=>x.photos).ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void Update(Member member)
        {
            context.Entry(member).State=EntityState.Modified;
        }
    }
}
