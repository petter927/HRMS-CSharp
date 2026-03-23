using System;
using System.Collections.Generic;

namespace Skygate_AspNet_MVC.Models;

public partial class LeaveRequest
{
    public string LeaveId { get; set; } = null!;

    public string ApplyId { get; set; } = null!;

    public string EmpId { get; set; } = null!;

    public string LeaveTypeId { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public decimal Hours { get; set; }

    public string Status { get; set; } = null!;

    public string? ApproverId { get; set; }

    public string? LeaveReason { get; set; }

    public string? Remark { get; set; }

    public virtual Employee Apply { get; set; } = null!;

    public virtual Employee Emp { get; set; } = null!;

    public virtual LeaveType LeaveType { get; set; } = null!;
}
