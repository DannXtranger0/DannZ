using DannZ.Models.DTO.Account;
using DannZ.Models;

namespace DannZ.Services
{
    public interface IUploadProfileImageService
    {
        Task UploadImage(string imageType, User user, EditUserDTO model);
    }
}
