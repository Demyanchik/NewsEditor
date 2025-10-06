using System;
using System.Collections.Generic;

namespace NewsEditor.Models.DB;

public partial class Users
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public int? Language { get; set; }

    public int Deleted { get; set; }

    public string? TimeCreated { get; set; }

    public virtual Languages? LanguageNavigation { get; set; }

    public virtual ICollection<News> News { get; set; } = new List<News>();
}
