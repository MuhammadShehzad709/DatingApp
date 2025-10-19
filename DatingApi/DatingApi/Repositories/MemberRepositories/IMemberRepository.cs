using DatingApi.ApiHelpers;
using DatingApi.Data.Entities;

namespace DatingApi.Repositories.MemberRepositories
{
    public interface IMemberRepository
    {
        void Update(Member member);
        Task<bool>SaveAllAsync();
        Task<PaginationReult<Member>> GetAsyMembersAsync(MemberParams memberParams);
        Task<Member?>GetMemberByIdAsync(Guid Id);
        Task<List<Photo>>GetPhotosForMemberAsync(Guid memberId);
        Task<Member?> GetMemberForUpdate(Guid Id);
    }
}
