using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Test.Entities.Models;
using Test.Service;
using Test.WebApi.ViewModel;
using Test.Entities.Helpers;

namespace Test.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService userService;
        private IHostingEnvironment _hostingEnvironment;
        private IConfiguration config;
        public AccountController(
            IHostingEnvironment hostingEnvironment,
            IUserService userService,
            IConfiguration config
            )
        {
            this.userService = userService;
            this.config = config;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost(nameof(Login))]
        public async Task<ActionResult> Login([FromBody] LoginViewModel loginView)
        {
            User user = await userService.CheckUserLogin( new User(){ Email = loginView.Email, Password = loginView.Password.Hash()});
            //User user = await userService.CheckUserLogin(new User() { Email = loginView.Email, Password = loginView.Password });
            if (user != null)
            {
                return Ok(new { user.Email, user.FullName, user.ImageUrl, IsAuthorized = true, Token = GetToken(user) });
            }
            return Ok(new { IsAuthorized = false, Data = (string)null, Message = "Invalid User" });
        }

        [HttpPost(nameof(Registration))]
        public async Task<IActionResult> Registration([FromForm] RegistrationViewModel formData)
        {

            if (formData.file != null)
            {
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }

                var filePath = Path.Combine(uploads, formData.file?.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formData.file.CopyToAsync(fileStream);
                }
                formData.ModelData.ImageUrl = formData.file?.FileName;
            }
            formData.ModelData.Password = formData.ModelData.Password.Hash();
            await userService.Registration(formData.ModelData);
            if(formData.ModelData.Id > 0)
                return Ok(new { status = true, Data = formData.ModelData, Message = "Registration Success!" });
            return Ok(new { status = false, Data = (string)null, Message = "Registration Faild!" });
        }

        private string GetToken(User userInfo)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim> {
                new Claim("userId", userInfo.Id.ToString()),
                new Claim(ClaimTypes.Email, userInfo.Email),
                new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString())
            };
            JwtSecurityToken token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:Issuer"],
                                            claims, notBefore: DateTime.UtcNow,
                                            expires: DateTime.Now.AddYears(1),
                                            signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
