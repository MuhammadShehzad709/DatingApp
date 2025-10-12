using DatingApi.Data.Entities;
using DatingApi.Dtos.PhotosDtos;

namespace DatingApi.Common.Extensions.Mappers.PhotoMapper
{
    public static class PhotoToGetPhotoDtoMapper
    {
        public static GetPhotoDto Map(this Photo photo)
        {
            return new GetPhotoDto(photo.Id, photo.Url, photo.publicID, photo.MemberID);
        }

        public static List<GetPhotoDto> MapList(this List<Photo> photo)
        {
            return photo.Select(p=> new GetPhotoDto(p.Id,p.Url,p.publicID,p.MemberID)).ToList();
        }
    }
}
