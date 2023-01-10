using System;
using System.Collections.Generic;

namespace Lesson23DataBaseFirst;

public partial class Timetable
{
    public Guid Id { get; set; }

    public Guid GroupId { get; set; }

    public Guid TeamIda { get; set; }

    public Guid TeamIdb { get; set; }

    public TimeSpan Time { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual Team TeamIdaNavigation { get; set; } = null!;

    public virtual Team TeamIdbNavigation { get; set; } = null!;
}
