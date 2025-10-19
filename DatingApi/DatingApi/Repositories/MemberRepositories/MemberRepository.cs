using DatingApi.ApiHelpers;
using DatingApi.Data;
using DatingApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace DatingApi.Repositories.MemberRepositories
{
    public class MemberRepository(AppDbContext context) : IMemberRepository
    {
        public async Task<PaginationReult<Member>> GetAsyMembersAsync(MemberParams memberParams)
        {
            var query=context.Members.AsQueryable();
            query = query.Where(x => x.Id != memberParams.CurrentMemberId);
            if(memberParams.Gender!= null)
            {
                query = query.Where(x => x.Gender == memberParams.Gender);
            }
            var minDob = DateOnly.FromDateTime(DateTime.Today.AddYears(-memberParams.MaxAge - 1));
            var maxDob = DateOnly.FromDateTime(DateTime.Today.AddYears(-memberParams.MinAge));
            query=query.Where(x=>x.DateOfBirth>=minDob && x.DateOfBirth<=maxDob);
            query = memberParams.OrderBy switch
            {
                "created" =>query.OrderByDescending(x=>x.CreatedAt),
                _=>query.OrderByDescending(x=>x.LastActive)
            };
            return await PaginationHelper.CreateAsync(query, memberParams.PageNumber,  memberParams.PageSize);
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
