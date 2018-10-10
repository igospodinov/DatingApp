using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegistorDto userForRegistorDto)
        {
            userForRegistorDto.Username = userForRegistorDto.Username.ToLower();

            if(await _repo.UserExists(userForRegistorDto.Username))
            {
                return BadRequest("Username already exist");
            }

            var userToCreate = new User
            {
                Username = userForRegistorDto.Username
            };

            var createdUser = await _repo.Register(userToCreate, userForRegistorDto.Password);

            return StatusCode(201);
        }
    }
}