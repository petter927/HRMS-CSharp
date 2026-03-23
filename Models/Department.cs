using System;
using System.Collections.Generic;

namespace Skygate_AspNet_MVC.Models;

public partial class Department
{
    public string DeptId { get; set; } = null!;

    public string DeptName { get; set; } = null!;

    public string? ParentId { get; set; }

    public string? ManagerId { get; set; }

    public virtual ICollection<Department> InverseParent { get; set; } = new List<Department>();

    public virtual Employee? Manager { get; set; }

    public virtual Department? Parent { get; set; }
}
