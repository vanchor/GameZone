using GameZone.Domain.Core.Entities;
using GameZone.Domain.Core.Enum;
using GameZone.Domain.Core.Response;

namespace GameZone.Service.Interfaces
{
    public interface IImageService
    {
        Task<BaseResponse<IEnumerable<Image>>> GetImagesAsync(int gameId, ImageType? imageType = null);
        Task<BaseResponse<Image>> GetImageAsync(int id);

        Task<BaseResponse<IEnumerable<Image>>> CreateImagesAsync(int gameId, IEnumerable<Image> images);

        Task<BaseResponse<Image>> UpdateImageAsync(int id, Image image);
        Task<BaseResponse<Image>> DeleteImageAsync(int id);
    }
}
