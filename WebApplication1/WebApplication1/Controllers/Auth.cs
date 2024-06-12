using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Utils;

namespace WebApplication1.Controllers;


[ApiController]
public class AuthenticationController : ControllerBase
{
    public class LoginRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    [HttpPost("/api/auth/login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Login(LoginRequest request)
    {
        if (String.IsNullOrWhiteSpace(request.Password) || String.IsNullOrWhiteSpace(request.Username))
        {
            return BadRequest(new Dictionary<string, object> {
                { "message", "Password or name cannot be empty!" },
                { "status", 400 }
            });
        }

        using (Ws1Context ctx = new Ws1Context())
        {
            User? user = ctx.Users.FirstOrDefault(u => u.Username == request.Username);
            if (user == null || user.Password != request.Password)
            {
                return BadRequest(new Dictionary<string, object> {
                    { "message", "Invalid username or password" },
                    { "status", 400 }
                });
            }

            return Ok(new
            {
                message = "Successfully logged in",
                status = 200,
                data = Marshall.Json(user),
            });
        }
    }


    public class RegisterRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public int Tel { get; set; }
    }

    [HttpPost("/api/auth/register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Register(RegisterRequest request)
    {
        using (Ws1Context ctx = new Ws1Context())
        {
            User? user = ctx.Users.FirstOrDefault(u =>
                u.Username == request.Username
                || u.Tel == request.Tel
            );

            if (user != null)
            {
                return BadRequest(new Dictionary<string, string> {
                    { "message", "A user with this username or tel already exists!" },
                    { "status", "400" },
                });
            }

            User newUser = new User()
            {
                UserId = ctx.Users.Count(),
                Username = request.Username,
                Password = request.Password,
                FullName = request.FullName,
                Tel = request.Tel
            };
            ctx.Users.Add(newUser);
            ctx.SaveChanges();

            return Created("/api/login", new Dictionary<string, string> {
                { "message", "User account created!" },
                { "status", "200" },
                { "data", $"{newUser.UserId}" }
            });
        }
    }
}
