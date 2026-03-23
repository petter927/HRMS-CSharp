using System;
using System.Collections.Generic;

namespace Skygate_AspNet_MVC.Models;

public partial class AttendanceLog
{
    public string LogId { get; set; } = null!;

    public string EmpId { get; set; } = null!;

    public DateTime LogTime { get; set; }

    public string LogType { get; set; } = null!;

    public string? DeviceId { get; set; }

    public string? Remark { get; set; }

    public virtual Employee Emp { get; set; } = null!;
}
