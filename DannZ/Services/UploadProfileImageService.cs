using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DannZ.Context;
using DannZ.Models;
using DannZ.Models.DTO.Account;

namespace DannZ.Services
{
    public class UploadProfileImageService : IUploadProfileImageService
    {
        private readonly MyDbContext _context;
        private readonly Cloudinary _cloudinary;

        public UploadProfileImageService(MyDbContext context, Cloudinary cloudinary)
        {
            _context = context;
            _cloudinary = cloudinary;
        }


        public async Task UploadImage(string imageType, User user, EditUserDTO model)
        {
            if (imageType == "Avatar")
            {
                //Create the new Avatar
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(model?.AvatarUrl!.FileName, model.AvatarUrl!.OpenReadStream()),
                    AssetFolder = "Avatars"
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                //Delete the old Avatar
                if (user.UserProfileImages.AvatarUrl != null)
                {
                    var deleteParams = new DeletionParams(user.UserProfileImages.AvatarPublicId);
                    var deleteResult = await _cloudinary.DestroyAsync(deleteParams);

                    user.UserProfileImages.AvatarUrl = uploadResult.SecureUrl.ToString();
                    user.UserProfileImages.AvatarPublicId = uploadResult.PublicId.ToString();
                }
                else if (user.UserProfileImages.CoverUrl != null && user.UserProfileImages.AvatarUrl == null)
                {
                    //Assign the new Avatar to the user
                    user.UserProfileImages.CoverUrl = uploadResult.SecureUrl.ToString();
                    user.UserProfileImages.CoverPublicId = uploadResult.PublicId.ToString();
                }
                else
                {
                    //Assign the new Avatar to the user
                    var uProfileImages = new UserProfileImages
                    {
                        UserId = user.Id,
                        AvatarUrl = uploadResult.SecureUrl.ToString(),
                        AvatarPublicId = uploadResult.PublicId.ToString(),
                    };
                    _context.UserProfileImages.Add(uProfileImages);
                }
            }
            else if (imageType == "Cover")
            {
                //Create the new Avatar
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(model?.CoverUrl!.FileName, model?.CoverUrl!.OpenReadStream()),
                    AssetFolder = "Covers"
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                //Delete the old Avatar
                if (user.UserProfileImages?.CoverUrl != null)
                {
                    var deleteParams = new DeletionParams(user.UserProfileImages.CoverPublicId);
                    var deleteResult = await _cloudinary.DestroyAsync(deleteParams);

                    user.UserProfileImages.CoverUrl = uploadResult.SecureUrl.ToString();
                    user.UserProfileImages.CoverPublicId = uploadResult.PublicId.ToString();
                }
                else if (user.UserProfileImages.AvatarUrl != null && user.UserProfileImages.CoverUrl == null)
                {
                    //Assign the new Avatar to the user
                    user.UserProfileImages.CoverUrl = uploadResult.SecureUrl.ToString();
                    user.UserProfileImages.CoverPublicId = uploadResult.PublicId.ToString();
                }
                else
                {
                    var uProfileImages = new UserProfileImages
                    {
                        UserId = user.Id,
                        CoverUrl = uploadResult.SecureUrl.ToString(),
                        CoverPublicId = uploadResult.PublicId.ToString(),
                    };
                    _context.UserProfileImages.Add(uProfileImages);

                }
            }
        }
    }
}
