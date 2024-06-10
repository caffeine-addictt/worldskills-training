using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Answer
{
    public int QuestionAnswerId { get; set; }

    public int QuestionOptionId { get; set; }

    public int QuestionId { get; set; }

    public int ExhibitorId { get; set; }

    public virtual User Exhibitor { get; set; } = null!;

    public virtual Question Question { get; set; } = null!;

    public virtual Option QuestionOption { get; set; } = null!;
}
