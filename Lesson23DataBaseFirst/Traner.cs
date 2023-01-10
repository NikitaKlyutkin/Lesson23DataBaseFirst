using System;
using System.Collections.Generic;

namespace Lesson23DataBaseFirst;

public partial class Traner
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public virtual ICollection<Team> Teams { get; } = new List<Team>();
}
