using System;
using System.Collections.Generic;

namespace Skygate_AspNet_MVC.Models;

public partial class SysLog
{
    public string LogId { get; set; } = null!;

    public string EmpId { get; set; } = null!;

    public string Action { get; set; } = null!;

    public string? TargetId { get; set; }

    public string? Details { get; set; }

    public DateTime LogTime { get; set; }

    public string? Ip { get; set; }

    public virtual Employee Emp { get; set; } = null!;
}
