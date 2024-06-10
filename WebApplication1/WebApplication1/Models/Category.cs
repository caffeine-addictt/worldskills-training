using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Workshop> Workshops { get; set; } = new List<Workshop>();
}
