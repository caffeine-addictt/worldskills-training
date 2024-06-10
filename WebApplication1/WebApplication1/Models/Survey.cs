using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Survey
{
    public int SurveyId { get; set; }

    public string Title { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
