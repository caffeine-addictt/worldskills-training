using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Option
{
    public int OptionId { get; set; }

    public string Title { get; set; } = null!;

    public int QuestionId { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual Question Question { get; set; } = null!;
}
