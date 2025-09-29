using System;
using System.Collections.Generic;

namespace NewsEditor.Models;

public partial class Languages
{
    public int Id { get; set; }

    public string Language { get; set; } = null!;

    public virtual ICollection<Users> Users { get; set; } = new List<Users>();
}
