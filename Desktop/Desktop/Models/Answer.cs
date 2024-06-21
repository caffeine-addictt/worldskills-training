using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Desktop.Models
{
    public partial class Answer
    {
        public int QuestionAnswerId { get; set; }
        public int QuestionOptionId { get; set; }
        public int QuestionId { get; set; }
        public int ExhibitorId { get; set; }

        public virtual User Exhibitor { get; set; }
        public virtual Question Question { get; set; }
        public virtual Option QuestionOption { get; set; }
    }
}
