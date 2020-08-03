using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskProject.Data;
using TaskProject.Models;
using TaskProject.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUsersRepository _repo;
        //private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        private readonly ApplicationDbContext _context;



        public UsersController(IUsersRepository repo,/* IMapper mapper,*/ UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            //_mapper = mapper;
            _repo = repo;
            _userManager = userManager;
            _context = context;

        }


        //send as query param
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(string id)
        {
           

            var user = await _repo.GetUser(id);
            return Ok(user);
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetAll<AppUser>();
            return Ok(users);
        }

        [HttpDelete("delete/{username}")]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> Delete(string username)
        {
            if (username == null)
            {
                return BadRequest("username should not be null!");
            }
            var user = await _userManager.FindByNameAsync(username);
            if (!(user == null))
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return Ok("UserDeleted");
                }
            }
            return NotFound();


        }




        [HttpPut("update")]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> Edit(RegisterViewModel rg)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var myuser = _repo.getmyUser(rg.Email);



                    myuser.FullName = rg.FullName;
                    myuser.Age = rg.Age;
                    myuser.Gender = rg.Gender;
                    myuser.BirthDate = rg.BirthDate;
                  

                     await _repo.Update(myuser);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest("Some properties are not valid"); // Status code: 400
        }




















    }
    }

