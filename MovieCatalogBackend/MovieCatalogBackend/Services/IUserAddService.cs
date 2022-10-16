using Microsoft.AspNetCore.Mvc;
using MovieCatalogBackend.Models;

namespace MovieCatalogBackend.Services
{
    public interface IUserAddService
    {
        public Task AddUser(UserRegisterModel model);
        
    }
}
