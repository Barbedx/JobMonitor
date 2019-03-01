using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using AspCoreAngular.Data;
using AspCoreAngular.Enums;
using AspCoreAngular.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AspCoreAngular.Controllers
{
    [Route("[action]")]
    //[Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly JwtIssuerOptions configuration;

        //private readonly IOptions<JwtIssuerOptions> configuration;
        [HttpGet]
        public async Task<List<ApplicationUser>> Users()
        {
            return await userManager.Users.ToListAsync();
        }

        [HttpGet]
        public async Task< ApplicationUser> Users(string id)
        {
            return await userManager.FindByIdAsync(id);
        }

        public AuthController(UserManager<ApplicationUser> userManager, IOptions<JwtIssuerOptions> configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration.Value;
        }

        //[Route("login")]
        //
        //[Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var result = await userManager.CreateAsync(user, model.Password);
            //if (result.Succeeded)
            //{
            //    await userManager.AddToRoleAsync(user, ApplicationRoles.User);
            //}
            return Ok(new { Username = user.UserName });
        }

        //[Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {

            if (!ModelState.IsValid)/// we realy need this????
            {
                return BadRequest(ModelState);
            }

            var user = await userManager.FindByNameAsync(model.UserName);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var claims =  new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                //  configuration["Jwt:SigningKey"]));
                var expireInMinutes = configuration.Expiration;


                var jwcToken = new JwtSecurityToken(
                    issuer:  configuration.Issuer,
                    audience: configuration.Audience,
                    expires: configuration.Expiration,
                    claims: claims,
                    signingCredentials:  configuration.SigningCredentials
                    );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwcToken),
                    expiration = jwcToken.ValidTo
                });

            }
            return Unauthorized();
        }
    }
}