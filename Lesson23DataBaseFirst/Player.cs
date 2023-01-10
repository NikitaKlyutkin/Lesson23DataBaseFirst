using System;
using System.Collections.Generic;

namespace Lesson23DataBaseFirst;

public partial class Player
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public double Age { get; set; }

    public Guid TeamsId { get; set; }

    public double Salary { get; set; }

    public virtual Team Teams { get; set; } = null!;
}
