using DatingApi.ApiHelpers;
using DatingApi.Common.Extensions;
using DatingApi.Dtos.MemberDtos;
using DatingApi.Services.memberServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApi.Controllers
{
    [Authorize]
    public class MembersController(IMemberService service):BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Getmember([FromQuery] PagingParams pagingParams)
        {
            return Ok(await service.GetAsyMembersAsync(pagingParams));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult>GetMember(Guid id)
        {
            return Ok(await service.GetMemberByIdAsync(id));
        }
        [HttpGet("{id}/photos")]
        public async Task<IActionResult> GetMemberPhotos(Guid id)
        {
            return Ok(await service.GetPhotosForMemberAsync(id));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateMember(UpdateMemberDto updateMemberDto)
        {
            var memberId = User.GetMemberId();
            return Ok(await service.UpdateMember(updateMemberDto,memberId));
        }
        [HttpPost("add-photo")]
        public async Task<IActionResult> AddPhoto([FromForm] IFormFile file)
        {
            var memberId = User.GetMemberId();
            return Ok(await service.AddPhoto(file, memberId));
        }
        [HttpPut("set-main-photo/{photoId}")]
        public async Task<IActionResult>SetMainPhoto(int photoId)
        {
            var memberID= User.GetMemberId();
            return Ok(await service.SetMainPhoto(photoId,memberID));
        }
        [HttpDelete("delete-photo/{photoId}")]
        public async Task<IActionResult> DeletePhoto(int photoId)
        {
            var memberID = User.GetMemberId();
            return Ok(await service.DeletePhoto(photoId, memberID));
        }


    }
}
