using GameZone.DAL.Repositories.Interfaces;
using GameZone.Domain.Core.Entities;
using GameZone.Domain.Core.Enum;
using GameZone.Domain.Core.Response;
using GameZone.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZone.Service.Implementations
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        public Task<BaseResponse<IEnumerable<Image>>> CreateImagesAsync(int gameId, IEnumerable<Image> images)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<Image>> DeleteImageAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<Image>> GetImageAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<IEnumerable<Image>>> GetImagesAsync(int gameId, ImageType? imageType = null)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<Image>> UpdateImageAsync(int id, Image image)
        {
            throw new NotImplementedException();
        }
    }
}
