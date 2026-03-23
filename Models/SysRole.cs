using System;
using System.Collections.Generic;

namespace Skygate_AspNet_MVC.Models;

public partial class SysRole
{
    public string RoleId { get; set; } = null!;

    public string RoleName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Employee> Emps { get; set; } = new List<Employee>();
}
