using System;
using System.Collections.Generic;

namespace Skygate_AspNet_MVC.Models;

public partial class SysAccount
{
    public string AccountId { get; set; } = null!;

    public string EmpId { get; set; } = null!;

    public string Account { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime? LastLogin { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Employee Emp { get; set; } = null!;
}
