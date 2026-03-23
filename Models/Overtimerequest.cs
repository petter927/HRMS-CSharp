using System;
using System.Collections.Generic;

namespace Skygate_AspNet_MVC.Models;

public partial class Overtimerequest
{
    public string Otid { get; set; } = null!;

    public string EmpId { get; set; } = null!;

    public DateOnly Otdate { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public decimal Hours { get; set; }

    public string Status { get; set; } = null!;

    public string? ApproverId { get; set; }

    public string Remark { get; set; } = null!;

    public virtual Employee? Approver { get; set; }

    public virtual Employee Emp { get; set; } = null!;
}
