using CleanArch.Application.Authentication;
using CleanArch.Application.Interfaces;
using CleanArch.Application.Services;
using CleanArch.Application.ViewModels;
using CleanArch.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace APICleanArch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IUserMktService _userMktService;

        public AuthenticationController(IUserMktService userMktService)
        {
            _userMktService = userMktService;
        }

        [HttpPost]
        [Route("Authentication")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] UserMkt model)
        {
            var user = await _userMktService.GetEmployeeByIdAsync(model.Id);

            if (user == null) return NotFound("User not found");

            if (user.Username != model.Username || user.Password != model.Password)
            {
                return BadRequest((new { message = "User or password invalid" }));
            }

            var token = TokenService.GenerateToken(user);

            user.Password = "";

            return new
            {
                user = user,
                token = token
            };
        }    
    }
}
