using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Desktop.Models
{
    public partial class Survey
    {
        public Survey()
        {
            Question = new HashSet<Question>();
        }

        public int SurveyId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Question> Question { get; set; }
    }
}
