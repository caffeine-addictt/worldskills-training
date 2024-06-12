using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Utils;

namespace WebApplication1.Controllers;


[ApiController]
public class SaloonController : ControllerBase
{
    public class SaloonCreateRequest
    {
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
    }

    [HttpPost("/api/saloon")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create(SaloonCreateRequest req)
    {
        req.Name = req.Name.Trim();

        if (String.IsNullOrWhiteSpace(req.Name))
            return BadRequest(new { Status = 400, Message = "Name is required!" });

        using (Ws1Context ctx = new Ws1Context())
        {
            // Check for valid userID
            User? usr = ctx.Users
                .FirstOrDefault(u => u.UserId == req.UserId);

            if (usr == null)
                return Unauthorized(new { Status = 401, Message = "Unauthorized" });

            if (ctx.Saloons.Any(s => s.Name == req.Name))
                return BadRequest(new { Status = 400, Message = "A saloon with this name already exists!" });

            Saloon newSaloon = new Saloon()
            {
                Name = req.Name,
                SaloonId = ctx.Saloons.Count()
            };

            ctx.Saloons.Add(newSaloon);
            ctx.SaveChanges();

            return Created($"/saloon/${newSaloon.SaloonId}", new {
                Status = 201,
                Message = "Created new workshop request",
                Data = Marshall.Json(newSaloon, 1)
            });
        }
    }


    [HttpGet("/api/saloon/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Get(int id)
    {
        using (Ws1Context ctx = new Ws1Context())
        {
            Saloon? saloon = ctx.Saloons.Include(s => s.Workshops).FirstOrDefault(s => s.SaloonId == id);
            if (saloon == null)
                return NotFound(new { Status = 404, Message = "Saloon does not eixst" });
            return Ok(new { 
                Status = 200,
                Message = "Retrieved saloon",
                Data = Marshall.Json(saloon, 1)
            });
        }
    }

    [HttpGet("/api/saloon")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ListSaloon()
    {
        using (Ws1Context ctx = new Ws1Context())
        {
            return Ok(new
            {
                Status = 200,
                Message = "Retrieved saloons",
                Data = ctx.Saloons
                    .Include(s => s.Workshops)
                    .Select(s => Marshall.Json(s, 0))
                    .ToList()
            });
        }
    }
}
