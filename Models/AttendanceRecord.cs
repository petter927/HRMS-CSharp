using System;
using System.Collections.Generic;

namespace Skygate_AspNet_MVC.Models;

public partial class AttendanceRecord
{
    public string RecordId { get; set; } = null!;

    public string EmpId { get; set; } = null!;

    public DateOnly WorkDate { get; set; }

    public DateTime? CheckInTime { get; set; }

    public DateTime? CheckOutTime { get; set; }

    public decimal? WorkHours { get; set; }

    public string? Status { get; set; }

    public decimal? OvertimeHours { get; set; }

    public string? Remark { get; set; }

    public virtual Employee Emp { get; set; } = null!;
}
