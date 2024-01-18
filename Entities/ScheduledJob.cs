using System;
using System.Collections.Generic;

namespace ArgaamSchedular.Entities;

public partial class ScheduledJob
{
    public int JobId { get; set; }

    public int? TypeId { get; set; }

    public string? Url { get; set; }

    public string? Frequency { get; set; }

    public string? ScheduleTime { get; set; }
}
