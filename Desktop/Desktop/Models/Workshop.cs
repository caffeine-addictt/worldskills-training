using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Desktop.Models
{
    public partial class Workshop
    {
        public int WorkshopId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Timeslot { get; set; }
        public int SaloonId { get; set; }
        public int CategoryId { get; set; }
        public int Status { get; set; }
        public DateTime RequestDate { get; set; }

        public virtual Category Category { get; set; }
        public virtual Saloon Saloon { get; set; }
        public virtual User User { get; set; }
    }
}
