using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository userRepo;

    public AuthController(IUserRepository userRepo)
    {
        this.userRepo = userRepo;
    }

    [HttpPost("login")]
    public ActionResult<UserDto> Login([FromBody] LoginRequest request)
    {
        try
        {
            User? user = userRepo.GetMany().FirstOrDefault(user => user.UserName == request.UserName);
            if (user is null || user.Password != request.Password)
            {
                return Unauthorized("Invalid user name or password.");
            }

            UserDto dto = new()
            {
                Id = user.Id,
                UserName = user.UserName
            };
            return Ok(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}
