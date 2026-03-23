using System;
using System.Collections.Generic;

namespace Skygate_AspNet_MVC.Models;

public partial class LeaveType
{
    public string LeaveTypeId { get; set; } = null!;

    public string LeaveName { get; set; } = null!;

    public bool IsPaid { get; set; }

    public decimal? MaxHoursPerYear { get; set; }

    public string? Remark { get; set; }

    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
}
