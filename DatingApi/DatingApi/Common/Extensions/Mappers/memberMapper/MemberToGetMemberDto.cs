using DatingApi.Data.Entities;
using DatingApi.Dtos.MemberDtos;

namespace DatingApi.Common.Extensions.Mappers.memberMapper;

public static class MemberToGetMemberDto
{
    public static GetMemberDto Map(this Member member)
    {
        return new GetMemberDto(
            member.Id,
            member.DisplayName,
            member.DateOfBirth,
            member.ImageUrl,
            member.CreatedAt,
            member.LastActive,
            member.Gender,
            member.Description,
            member.City,
            member.Country);
    }
    public static List<GetMemberDto> MapList(this List<Member> member)
    {
        return member.Select(m => Map(m)).ToList();
    }

}