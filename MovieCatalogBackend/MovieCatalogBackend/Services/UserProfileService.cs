using Microsoft.AspNetCore.Mvc;
using MovieCatalogBackend.Context;
using MovieCatalogBackend.Models.DTO;

namespace MovieCatalogBackend.Services
{
    public class UserProfileService : IUserProfileService
    {
        private MovieCatalogDbContext _context;
        public UserProfileService(MovieCatalogDbContext context)
        {
            _context = context;
        }
        public async Task<ProfileModel> GetUserProfile(string id)
        {
            var user = _context.Users.Find(Guid.Parse(id));
            if(user == null)
            {
                throw new Exception("User doesn't exists");
            }
            return new ProfileModel
            {
                AvatarLink = user.Avatar,
                BirthDate = user.BirthDate,
                Email = user.Email,
                Gender = user.Gender,
                Id = user.Id,
                Name = user.Name,
                NickName = user.UserName,
            };
        }

        public async Task ChangeUserProfile(ProfileModel model, string id)
        {
            var user =  _context.Users.Find(Guid.Parse(id));
            var userWithTheSameUserName=_context.Users.FirstOrDefault(x => x.UserName==model.NickName && x.Id!= new Guid(id));
            var userWithTheSameEmail = _context.Users.FirstOrDefault(x => x.Email == model.Email && x.Id != new Guid(id));
            var userWithTheSameId = _context.Users.FirstOrDefault(x => x.Id == model.Id && new Guid(id)!=model.Id);
            if (userWithTheSameEmail != null)
            {
                throw new Exception("User with this Email already exists!");
            }
            if(userWithTheSameUserName != null)
            {
                throw new Exception("User with this NickName already exists!");
            }
            if (userWithTheSameId != null)
            {
                throw new Exception("User with this ID already exists!");
            }
            user.Id=model.Id;
            user.UserName = model.NickName;
            user.Avatar = model.AvatarLink;
            user.Name = model.Name;
            user.Email = model.Email;
            user.BirthDate=model.BirthDate;
            user.Gender = model.Gender;
            await _context.SaveChangesAsync();
        }
    }
}
