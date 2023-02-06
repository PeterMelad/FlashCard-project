using JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class SecurityController : ControllerBase
    {
        UserManager<IdentityUser> _usermanager;
        SignInManager<IdentityUser> _signin;
        RoleManager<IdentityRole> _roleManager;
        //Models.JWT jwtcont;
        IConfiguration Configuration;
        public SecurityController(SignInManager<IdentityUser> _signin, IConfiguration Configuration,/*Models.JWT jwtcont,*/ UserManager<IdentityUser> _usermanager, RoleManager<IdentityRole> _roleManager)
        {
            this._usermanager = _usermanager;
            this._roleManager = _roleManager;
            this._signin = _signin;
            this.Configuration = Configuration;
           // this.jwtcont = jwtcont;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Loggingin(Login login)
        {
            if (!ModelState.IsValid) return BadRequest();
            var user = await _usermanager.FindByNameAsync(login.UserName);
            if (user == null) return BadRequest("Invalid data");
            var result = await _signin.PasswordSignInAsync(user, login.Password, false, false);
            if (result.Succeeded)
            {
                Models.JWT jwtcon = new Models.JWT
                {
                    Key = Configuration["JWT:Key"],
                    Issuer=Configuration["JWT:Issuer"],
                    Audience=Configuration["JWT:Audience"],
                    DurationInDays=double.Parse(Configuration["JWT:DurationInDays"])
                };
                JwtOperations jwt = new JwtOperations(_usermanager);
                var token=jwt.GenToken(user, jwtcon);

                return Ok(token);
            }
            return BadRequest();
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(Register register)
        {
          
            if (!ModelState.IsValid) return BadRequest();
            var user = new IdentityUser { UserName = register.info.UserName, Email = register.Email };
            var result = await _usermanager.CreateAsync(user, register.info.Password);
            if (result.Succeeded)
            {
                await _usermanager.AddToRoleAsync(user, "User");
                Models.JWT jwtcon = new Models.JWT
                {
                    Key = Configuration["JWT:Key"],
                    Issuer = Configuration["JWT:Issuer"],
                    Audience = Configuration["JWT:Audience"],
                    DurationInDays = double.Parse(Configuration["JWT:DurationInDays"])
                };
                JwtOperations jwt = new JwtOperations(_usermanager);
                var token = jwt.GenToken(user, jwtcon);
                return Ok(token);
            }
            return BadRequest();
        }

    }
}
