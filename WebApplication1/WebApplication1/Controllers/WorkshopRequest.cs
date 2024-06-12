using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;
using WebApplication1.Utils;

namespace WebApplication1.Controllers;


[ApiController]
public class WorkshopRequestController : ControllerBase
{
    public class WorkshopRequestCreateRequest
    {
        public int UserId { get; set; }

        public string Title { get; set; } = null!;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        /// <summary>
        /// 0 - 1000-1200
        /// 1 - 1200-1400
        /// 2 - 1400-1600
        /// </summary>
        public int Timeslot { get; set; }
        public string Category { get; set; } = null!;
        public string Saloon { get; set; } = null!;
    }

    [HttpPost("/api/workshop-request")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create(WorkshopRequestCreateRequest req)
    {
        req.Title = req.Title.Trim().ToLower();
        req.Category = req.Category.Trim().ToLower();

        if (String.IsNullOrWhiteSpace(req.Title))
            return BadRequest(new { Status = 400, Message = "Title is required!" });
        if (String.IsNullOrWhiteSpace(req.Category))
            return BadRequest(new { Status = 400, Message = "Category is required!" });
        if (0 > req.Timeslot || req.Timeslot > 2)
            return BadRequest(new { Status = 400, Message = "Timeslot is between 0 and 2" });
        if (req.EndDate < req.StartDate)
            return BadRequest(new { Status = 400, Message = "End Date is before Start Date" });

        using (Ws1Context ctx = new Ws1Context())
        {
            // Check for valid userID
            User? usr = ctx.Users
                .Include(u => u.Workshops)
                .ThenInclude(w => w.Saloon)
                .FirstOrDefault(u => u.UserId == req.UserId);

            if (usr == null)
                return Unauthorized(new { Status = 401, Message = $"Unauthorized" });

            // Check for valid saloonID
            Saloon? saloon = ctx.Saloons
                .Include(s => s.Workshops)
                .FirstOrDefault(s => s.Name == req.Saloon);

            if (saloon == null)
                return BadRequest(new { Status = 400, Message = $"Saloon {req.Saloon} does not exist" });
            if (saloon.Workshops.Any(w =>
                w.Status == 1
                && Time.Overlaps(w.StartDate, w.EndDate, req.StartDate, req.EndDate)
            ))
                return BadRequest(new { Status = 403, Message = "There is already a workshop scheduled at this time at this saloon" });

            // Category
            Category? cat = ctx.Categories.FirstOrDefault(c => c.CategoryName.ToLower() == req.Category);
            if (cat == null)
            {
                cat = new Category() {
                    CategoryId = ctx.Categories.Count(),
                    CategoryName = req.Category
                };
            }

            Workshop ws = new Workshop()
            {
                WorkshopId = ctx.Workshops.Count(),
                Title = req.Title,
                Timeslot = req.Timeslot,
                StartDate = req.StartDate,
                EndDate = req.EndDate,
                Category = cat
            };

            usr.Workshops.Add(ws);
            cat.Workshops.Add(ws);
            saloon.Workshops.Add(ws);
            ctx.SaveChanges();

            return Created($"/workshop-request/${ws.WorkshopId}", new {
                Status = 201,
                Message = "Created new workshop request",
                Data = Marshall.Json(ws, 1)
            });
        }
    }
}
