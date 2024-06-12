using WebApplication1.Models;

namespace WebApplication1.Utils;


/// <summary>
/// JSON marshalls objects
/// </summary>
public static class Marshall
{
    public static object? Json(User? u, int depth = 0)
        => depth < 0 ? null
            : new
            {
                UserId = u!.UserId,
                Tel = u.Tel,
                Username = u.Username,
                Password = u.Password,
                FullName = u.FullName,
            };

    public static object? Json(Workshop? w, int depth = 0)
        => depth < 0 ? null
            : new
            {
                WorkshopId = w!.WorkshopId,
                EndDate = w.EndDate,
                StartDate = w.StartDate,
                Timeslot = Timeslot.AsString(w.Timeslot),
                Title = w.Title,
                Status = w.Status == 0 ? "Pending" : (w.Status == 1 ? "Accepted" : "Rejected"),
                RequestDate = w.RequestDate,
                Category = Json(w.Category, depth - 1),
                Saloon = Json(w.Saloon, depth - 1),
            };

    public static object? Json(Category? c, int depth = 0)
        => depth < 0 ? null
            : new
            {
                CategoryId = c!.CategoryId,
                CategoryName = c.CategoryName,
                Workshops = depth > 0
                    ? c.Workshops.Select(w => Json(w, depth - 1))
                    : [],
            };

    public static object? Json(Saloon? s, int depth = 0)
        => depth < 0 ? null
            : new
            {
                SaloonId = s!.SaloonId,
                SaloonName = s.Name,
                Workshops = depth > 0
                    ? s.Workshops.Select(w => Json(w, depth - 1))
                    : [],
            };
}
