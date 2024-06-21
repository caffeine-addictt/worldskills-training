using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Desktop.Models
{
    public partial class User
    {
        public User()
        {
            Answer = new HashSet<Answer>();
            Workshop = new HashSet<Workshop>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public int? Tel { get; set; }
        public string Salt { get; set; }

        public virtual ICollection<Answer> Answer { get; set; }
        public virtual ICollection<Workshop> Workshop { get; set; }
    }
}
