using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Models;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo) => _repo = repo;

        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password)
        {
            username = username.ToLower();
             
            if(await _repo.UserExists(username)) 
                return BadRequest("User Name already Exists");
            
            var userToCreate = new User { UserName = username };
            var createdUser = await _repo.Register(userToCreate, password);

            return StatusCode(201);
        }

    
    }
}