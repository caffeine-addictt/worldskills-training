using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;
using WebApplication1.Utils;

namespace WebApplication1.Controllers;


[ApiController]
public class WorkshopController : ControllerBase
{
    [HttpGet("/api/workshop")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetList([FromQuery] int? status, [FromQuery] int? userid)
    {
        using (Ws1Context ctx = new Ws1Context())
        {
            IQueryable<Workshop> query = ctx.Workshops
                .Include(w => w.Category)
                .Include(w => w.Saloon);

            if (status != null)
                query = query.Where(w => w.Status == status);
            if (userid != null)
                query = query.Where(w => w.UserId == userid);

            return Ok(new
            {
                Status = 200,
                Message = "Successfully fetched workshops!",
                Data = query
                    .Select(w => Marshall.Json(w, 1))
                    .ToList(),
            });
        }
    }
}
