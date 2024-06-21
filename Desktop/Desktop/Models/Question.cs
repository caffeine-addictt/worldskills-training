using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Desktop.Models
{
    public partial class Question
    {
        public Question()
        {
            Answer = new HashSet<Answer>();
            Option = new HashSet<Option>();
        }

        public int QuestionId { get; set; }
        public int SurveyId { get; set; }
        public string Title { get; set; }

        public virtual Survey Survey { get; set; }
        public virtual ICollection<Answer> Answer { get; set; }
        public virtual ICollection<Option> Option { get; set; }
    }
}
