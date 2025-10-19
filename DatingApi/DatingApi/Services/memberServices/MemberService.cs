using DatingApi.ApiHelpers;
using DatingApi.Common.Extensions.Mappers.memberMapper;
using DatingApi.Common.Extensions.Mappers.PhotoMapper;
using DatingApi.Common.Response;
using DatingApi.Data.Entities;
using DatingApi.Dtos.MemberDtos;
using DatingApi.Dtos.PhotosDtos;
using DatingApi.Repositories.MemberRepositories;
using DatingApi.Services.PhotoService;
using Microsoft.OpenApi.Services;

namespace DatingApi.Services.memberServices
{
    public class MemberService(IMemberRepository repository, IPhotoService photoService) : IMemberService
    {

        public async Task<ApiResponse<PaginationReult<GetMemberDto>>> GetAsyMembersAsync(MemberParams memberParams)
        {
            var query = await repository.GetAsyMembersAsync(memberParams);
            return ApiResponse<PaginationReult<GetMemberDto>>.SuccessResponse(
                 new PaginationReult<GetMemberDto>
                 {
                     MetaData = query.MetaData,
                     Items = query.Items.MapList()
                 }
            );
        }

        public async Task<ApiResponse<GetMemberDto>> GetMemberByIdAsync(Guid Id)
        {
            var data = await repository.GetMemberByIdAsync(Id);
            if (data is null)
            {
                return ApiResponse<GetMemberDto>.ErrorResponse("The User No Exist");
            }
            return ApiResponse<GetMemberDto>.SuccessResponse(data.Map());
        }
        public async Task<ApiResponse<List<GetPhotoDto>>> GetPhotosForMemberAsync(Guid memberId)
        {
            return ApiResponse<List<GetPhotoDto>>.SuccessResponse((await repository.GetPhotosForMemberAsync(memberId)).MapList());
        }

        public Task<bool> SaveAllAsync()
        {
            return repository.SaveAllAsync();
        }

        public void Update(Member member)
        {
            repository.Update(member);
        }
        public async Task<ApiResponse<int>> UpdateMember(UpdateMemberDto updateMemberDto, string Id)
        {
            var member = await repository.GetMemberForUpdate(Guid.Parse(Id));
            if (member == null) return ApiResponse<int>.ErrorResponse("Could not get member");
            member.DisplayName = updateMemberDto.DisplayName ?? member.DisplayName;
            member.Description = updateMemberDto.Description ?? member.Description;
            member.City = updateMemberDto.City ?? member.City;
            member.Country = updateMemberDto.Country ?? member.Country;
            if (await SaveAllAsync()) return ApiResponse<int>.SuccessResponse(1);
            return ApiResponse<int>.ErrorResponse("Faild to updateMember");
        }
        public async Task<ApiResponse<GetPhotoDto>> AddPhoto(IFormFile file, string Id)
        {
            var member = await repository.GetMemberForUpdate(Guid.Parse(Id));
            if (member is null) return ApiResponse<GetPhotoDto>.ErrorResponse("Can Not Update Member");
            var result = await photoService.UploadPhotoAsync(file);
            if (result.Error != null) return ApiResponse<GetPhotoDto>.ErrorResponse(result.Error.Message);
            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                publicID = result.PublicId,
                MemberID = Guid.Parse(Id),
            };
            if (member.ImageUrl == null)
            {
                member.ImageUrl = photo.Url;
                member.User.ImageUrl = photo.Url;
            }
            member.photos.Add(photo);
            if (await SaveAllAsync()) return ApiResponse<GetPhotoDto>.SuccessResponse(photo.Map());
            return ApiResponse<GetPhotoDto>.ErrorResponse("problme Adding Photo");
        }

        public async Task<ApiResponse<int>> SetMainPhoto(int Id, string memberId)
        {
            var member = await repository.GetMemberForUpdate(Guid.Parse(memberId));
            if (member is null) return ApiResponse<int>.ErrorResponse("Cannot get member from the token");
            var photo = member.photos.SingleOrDefault(x => x.Id == Id);
            if (member.ImageUrl == photo?.Url || photo == null) return ApiResponse<int>.ErrorResponse("Cannot set this as main photo");
            member.ImageUrl = photo?.Url;
            member.User.ImageUrl = photo?.Url;
            if (await repository.SaveAllAsync()) return ApiResponse<int>.SuccessResponse(1);
            return ApiResponse<int>.ErrorResponse("Problem Setting Main photo");
        }

        public async Task<ApiResponse<int>> DeletePhoto(int photoId, string memberId)
        {
            var member = await repository.GetMemberForUpdate(Guid.Parse(memberId));
            if (member is null) return ApiResponse<int>.ErrorResponse("Cannot get member from the token");
            var photo = member.photos.SingleOrDefault(x => x.Id == photoId);
            if (photo is null || photo.Url == member.ImageUrl) return ApiResponse<int>.ErrorResponse("This photo cannot be deleted");
            if (photo.publicID is not null)
            {
                var result = await photoService.DeletePhotoAsync(photo.publicID);
                if (result.Error != null) return ApiResponse<int>.ErrorResponse(result.Error.Message);
            }
            member.photos.Remove(photo);
            if (await repository.SaveAllAsync()) return ApiResponse<int>.SuccessResponse(1);
            return ApiResponse<int>.ErrorResponse("Problem deleting photo");
        }
    }
}
