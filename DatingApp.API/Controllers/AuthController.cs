using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Models;
using DatingApp.API.DTOs;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo) => _repo = repo;

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO UserForRegisterDTO)
        {
            UserForRegisterDTO.Username = UserForRegisterDTO.Username.ToLower();
             
            if(await _repo.UserExists(UserForRegisterDTO.Username)) 
                return BadRequest("User Name already Exists");
            
            var userToCreate = new User { UserName = UserForRegisterDTO.Username };
            var createdUser = await _repo.Register(userToCreate, UserForRegisterDTO.Password);

            return StatusCode(201);
        }

    
    }
}