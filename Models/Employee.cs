using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Skygate_AspNet_MVC.Models;

public partial class Employee
{
    [Display(Name = "員工編號")]
    public string EmpId { get; set; } = null!;

    [Required(ErrorMessage = "員工姓名必須填寫")]
    [Display(Name ="員工姓名")]
    public string EmpName { get; set; } = null!;
    [Required(ErrorMessage = "部門編號必須填寫")]
    [Display(Name = "部門編號")]
    public string DeptId { get; set; } = null!;
    [Required(ErrorMessage = "職稱必須填寫")]
    [Display(Name = "職稱編號")]
    public string Title { get; set; } = null!;
    
    [Display(Name = "主管員工編號")]
    public string? SupervisorId { get; set; }
    [Display(Name = "員工狀態")]
    public string Status { get; set; } = null!;
    [Display(Name = "入職日期")]
    public DateOnly HireDate { get; set; }
    [Display(Name = "離職日期")]
    public DateOnly? LeaveDate { get; set; }
    [Required(ErrorMessage = "Email必須填寫")]
    [Display(Name = "Email")]
    public string Email { get; set; } = null!;
    [Required(ErrorMessage = "聯絡電話必須填寫")]
    [Display(Name = "聯絡電話")]
    public string Phone { get; set; } = null!;

    public virtual ICollection<AttendanceLog> AttendanceLogs { get; set; } = new List<AttendanceLog>();

    public virtual ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual ICollection<LeaveRequest> LeaveRequestApplies { get; set; } = new List<LeaveRequest>();

    public virtual ICollection<LeaveRequest> LeaveRequestEmps { get; set; } = new List<LeaveRequest>();

    public virtual ICollection<Overtimerequest> OvertimerequestApprovers { get; set; } = new List<Overtimerequest>();

    public virtual ICollection<Overtimerequest> OvertimerequestEmps { get; set; } = new List<Overtimerequest>();

    public virtual ICollection<SysAccount> SysAccounts { get; set; } = new List<SysAccount>();

    public virtual ICollection<SysLog> SysLogs { get; set; } = new List<SysLog>();

    public virtual ICollection<SysNotification> SysNotifications { get; set; } = new List<SysNotification>();

    public virtual ICollection<SysRole> Roles { get; set; } = new List<SysRole>();
}
