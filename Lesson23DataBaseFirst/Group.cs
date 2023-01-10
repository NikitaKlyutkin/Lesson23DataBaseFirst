using System;
using System.Collections.Generic;

namespace Lesson23DataBaseFirst;

public partial class Group
{
    public Guid Id { get; set; }

    public Guid TeamsId { get; set; }

    public virtual Team Teams { get; set; } = null!;

    public virtual ICollection<Timetable> Timetables { get; } = new List<Timetable>();
}
