using System;
using System.Collections.Generic;

namespace NewsEditor.Models;

public partial class News
{
    public int Id { get; set; }

    public string Header { get; set; } = null!;

    public byte[]? Image { get; set; }

    public string? SubHeader { get; set; }

    public string? Text { get; set; }

    public int? UserId { get; set; }

    public string? TimeCreated { get; set; }

    public virtual Users? User { get; set; }
}
