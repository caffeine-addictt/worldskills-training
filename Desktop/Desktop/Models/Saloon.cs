using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Desktop.Models
{
    public partial class Saloon
    {
        public Saloon()
        {
            Workshop = new HashSet<Workshop>();
        }

        public int SaloonId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Workshop> Workshop { get; set; }
    }
}
