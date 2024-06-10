using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Workshop
{
    public int WorkshopId { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int Timeslot { get; set; }

    public int SaloonId { get; set; }

    public int CategoryId { get; set; }

    public int Status { get; set; }

    public DateOnly RequestDate { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Saloon Saloon { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
