using Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _serviceUser;


        public UserController(IUserService service)
        {
            _serviceUser = service;
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserDtoOut>>> GetUsers()
        {
            var users = await _serviceUser.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDtoOut>> GetUser(int id)
        {
            var user = await _serviceUser.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }



        /*[HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> UpdateUser(int id, User user)
        {
            var isExistingUser = await _serviceUser.GetByIdAsync(id);
            if (isExistingUser == null)
            {
                return NotFound();
            }
            
            isExistingUser.Name = user.Name;
            isExistingUser.Mail = user.Mail;
            isExistingUser.Password = user.Password;
            isExistingUser.Telephone = user.Telephone;  


            await _serviceUser.UpdateAsync(isExistingUser);
            return NoContent();
        }*/


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _serviceUser.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _serviceUser.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("init")]
        [AllowAnonymous]
         public async Task<IActionResult> InitData()
        {
            await _serviceUser.InitDataAsync();
            return Ok("Datos de usuario iniciados correctamente");
        }

    }
}