using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Question
{
    public int QuestionId { get; set; }

    public int SurveyId { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual ICollection<Option> Options { get; set; } = new List<Option>();

    public virtual Survey Survey { get; set; } = null!;
}
