using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskProject.Models;

namespace TaskProject.Services.Interfaces
{
    public interface IUsersRepository
    {
       Task< List<AppUser>> GetAll<T>() where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> Update(AppUser user);
        AppUser getmyUser(string email);
        Task<bool> SaveAll();
        Task<AppUser> GetUser(string Email);

    }
}
