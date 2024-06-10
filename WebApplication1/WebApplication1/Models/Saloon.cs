using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Saloon
{
    public int SaloonId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Workshop> Workshops { get; set; } = new List<Workshop>();
}
