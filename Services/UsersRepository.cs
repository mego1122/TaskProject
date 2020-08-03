using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskProject.Data;
using TaskProject.Models;
using TaskProject.Services.Interfaces;

namespace TaskProject.Services
{
    public class UsersRepository:IUsersRepository
    {
        private readonly ApplicationDbContext _context;
        public UsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }
  

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<List<AppUser>> GetAll<T>() where T : class
        {
            return await _context.Users.ToListAsync<AppUser>();
        }

        public async Task<AppUser> GetUser(string id)
        {
            var query = _context.Users.AsQueryable();

            if (id != null)
                query = query.IgnoreQueryFilters();

            var user = await query.FirstOrDefaultAsync(u => u.Id.Contains(id));

            return user;
        }




        public async Task<bool> Update(AppUser user)
        {


            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public AppUser getmyUser(string email)
        {
           return   _context.Users.SingleOrDefault(x => x.Email == email);
        }



    }
}
