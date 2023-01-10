using System;
using System.Collections.Generic;

namespace Lesson23DataBaseFirst;

public partial class Team
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public double Rate { get; set; }

    public virtual ICollection<Group> Groups { get; } = new List<Group>();

    public virtual ICollection<Player> Players { get; } = new List<Player>();

    public virtual ICollection<Timetable> TimetableTeamIdaNavigations { get; } = new List<Timetable>();

    public virtual ICollection<Timetable> TimetableTeamIdbNavigations { get; } = new List<Timetable>();

    public virtual ICollection<Traner> Traners { get; } = new List<Traner>();
}
