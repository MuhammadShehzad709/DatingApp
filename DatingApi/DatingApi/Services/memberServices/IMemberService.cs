using DatingApi.ApiHelpers;
using DatingApi.Common.Response;
using DatingApi.Data.Entities;
using DatingApi.Dtos.MemberDtos;
using DatingApi.Dtos.PhotosDtos;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DatingApi.Services.memberServices
{
    public interface IMemberService
    {

        void Update(Member member);
        Task<bool> SaveAllAsync();
        Task<ApiResponse<PaginationReult<Member>>> GetAsyMembersAsync(PagingParams pagingParams);
        Task<ApiResponse<GetMemberDto>> GetMemberByIdAsync(Guid Id);
        Task<ApiResponse<List<GetPhotoDto>>> GetPhotosForMemberAsync(Guid memberId);
        Task<ApiResponse<GetPhotoDto>> AddPhoto(IFormFile file,string Id);
        Task<ApiResponse<int>> UpdateMember(UpdateMemberDto updateMemberDto, string Id);
        Task<ApiResponse<int>> SetMainPhoto(int Id,string memberId);
        Task<ApiResponse<int>> DeletePhoto(int photoId, string memberId);
    }
}
