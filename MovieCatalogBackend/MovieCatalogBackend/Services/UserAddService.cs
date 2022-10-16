using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCatalogBackend.Context;
using MovieCatalogBackend.Models;

namespace MovieCatalogBackend.Services
{
    public class UserAddService:IUserAddService
    {
       private MovieCatalogDbContext _context; 
       public UserAddService(MovieCatalogDbContext context)
        {
            _context = context;
        }
        public async Task AddUser(UserRegisterModel model)
        {

            var result = _context.Users.FirstOrDefault(p => p.UserName == model.UserName);
            var result2 = _context.Users.FirstOrDefault(p => p.Email == model.Email);
            if (result == null && result2 == null)
            {
                await _context.Users.AddAsync(new UserDbModel
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password,
                    UserName = model.UserName,
                    BirthDate = model.BirthDate,
                    Gender = model.Gender,
                });
                await _context.SaveChangesAsync();
            }
            else if(result != null)
            {
                throw new ArgumentException("User with this UserName already exists!");
            }
            else
            {
                throw new ArgumentException("User with this Email already exists!");
            }
        }
    }
}
