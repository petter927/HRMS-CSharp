using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Skygate_AspNet_MVC.Models;

public partial class SkyGateContext : DbContext
{
    public SkyGateContext()
    {
    }

    public SkyGateContext(DbContextOptions<SkyGateContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AttendanceLog> AttendanceLogs { get; set; }

    public virtual DbSet<AttendanceRecord> AttendanceRecords { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }

    public virtual DbSet<LeaveType> LeaveTypes { get; set; }

    public virtual DbSet<Overtimerequest> Overtimerequests { get; set; }

    public virtual DbSet<SysAccount> SysAccounts { get; set; }

    public virtual DbSet<SysLog> SysLogs { get; set; }

    public virtual DbSet<SysNotification> SysNotifications { get; set; }

    public virtual DbSet<SysRole> SysRoles { get; set; }

    public virtual DbSet<SystemAttachment> SystemAttachments { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
       //SysUserRole => optionsBuilder.UseSqlServer("Server=.\\SQL2022;Database=SkyGate;User Id=sa;Password=1qaz@wsx;Integrated Security=false;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AttendanceLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__Attendan__5E5499A89EEA92E8");

            entity.ToTable("AttendanceLog");

            entity.Property(e => e.LogId)
                .HasMaxLength(50)
                .HasColumnName("LogID");
            entity.Property(e => e.DeviceId)
                .HasMaxLength(50)
                .HasColumnName("DeviceID");
            entity.Property(e => e.EmpId)
                .HasMaxLength(50)
                .HasColumnName("EmpID");
            entity.Property(e => e.LogTime).HasColumnType("datetime");
            entity.Property(e => e.LogType).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);

            entity.HasOne(d => d.Emp).WithMany(p => p.AttendanceLogs)
                .HasForeignKey(d => d.EmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AttendanceLog_Employee");
        });

        modelBuilder.Entity<AttendanceRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__Attendan__FBDF78C90F2030B8");

            entity.ToTable("AttendanceRecord");

            entity.Property(e => e.RecordId)
                .HasMaxLength(50)
                .HasColumnName("RecordID");
            entity.Property(e => e.CheckInTime).HasColumnType("datetime");
            entity.Property(e => e.CheckOutTime).HasColumnType("datetime");
            entity.Property(e => e.EmpId)
                .HasMaxLength(50)
                .HasColumnName("EmpID");
            entity.Property(e => e.OvertimeHours).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.WorkHours).HasColumnType("decimal(18, 4)");

            entity.HasOne(d => d.Emp).WithMany(p => p.AttendanceRecords)
                .HasForeignKey(d => d.EmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AttendanceRecord_Employee");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DeptId).HasName("PK__Departme__0148818E29623D30");

            entity.ToTable("Department");

            entity.Property(e => e.DeptId)
                .HasMaxLength(50)
                .HasColumnName("DeptID");
            entity.Property(e => e.DeptName).HasMaxLength(50);
            entity.Property(e => e.ManagerId)
                .HasMaxLength(50)
                .HasColumnName("ManagerID");
            entity.Property(e => e.ParentId)
                .HasMaxLength(50)
                .HasColumnName("ParentID");

            entity.HasOne(d => d.Manager).WithMany(p => p.Departments)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK_Department_Employee");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_Department_Parent");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK__Employee__AF2DBA793273C383");

            entity.ToTable("Employee");

            entity.Property(e => e.EmpId)
                .HasMaxLength(50)
                .HasColumnName("EmpID");
            entity.Property(e => e.DeptId)
                .HasMaxLength(50)
                .HasColumnName("DeptID");
            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.EmpName).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("0");
            entity.Property(e => e.SupervisorId)
                .HasMaxLength(50)
                .HasColumnName("SupervisorID");
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasMany(d => d.Roles).WithMany(p => p.Emps)
                .UsingEntity<Dictionary<string, object>>(
                    "SysUserRole",
                    r => r.HasOne<SysRole>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_SysUserRole_SysRole"),
                    l => l.HasOne<Employee>().WithMany()
                        .HasForeignKey("EmpId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_SysUserRole_Employee"),
                    j =>
                    {
                        j.HasKey("EmpId", "RoleId");
                        j.ToTable("SysUserRole");
                        j.IndexerProperty<string>("EmpId")
                            .HasMaxLength(50)
                            .HasColumnName("EmpID");
                        j.IndexerProperty<string>("RoleId")
                            .HasMaxLength(50)
                            .HasColumnName("RoleID");
                    });
        });

        modelBuilder.Entity<LeaveRequest>(entity =>
        {
            entity.HasKey(e => e.LeaveId).HasName("PK__LeaveReq__796DB9793AF205C0");

            entity.ToTable("LeaveRequest");

            entity.Property(e => e.LeaveId)
                .HasMaxLength(50)
                .HasColumnName("LeaveID");
            entity.Property(e => e.ApplyId)
                .HasMaxLength(50)
                .HasColumnName("ApplyID");
            entity.Property(e => e.ApproverId)
                .HasMaxLength(50)
                .HasColumnName("ApproverID");
            entity.Property(e => e.EmpId)
                .HasMaxLength(50)
                .HasColumnName("EmpID");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.Hours).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.LeaveReason).HasMaxLength(250);
            entity.Property(e => e.LeaveTypeId).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Apply).WithMany(p => p.LeaveRequestApplies)
                .HasForeignKey(d => d.ApplyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LeaveRequest_Employee2");

            entity.HasOne(d => d.Emp).WithMany(p => p.LeaveRequestEmps)
                .HasForeignKey(d => d.EmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LeaveRequest_Employee1");

            entity.HasOne(d => d.LeaveType).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.LeaveTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LeaveRequest_LeaveType");
        });

        modelBuilder.Entity<LeaveType>(entity =>
        {
            entity.HasKey(e => e.LeaveTypeId).HasName("PK__LeaveTyp__43BE8FF4461CC65D");

            entity.ToTable("LeaveType");

            entity.Property(e => e.LeaveTypeId)
                .HasMaxLength(50)
                .HasColumnName("LeaveTypeID");
            entity.Property(e => e.LeaveName).HasMaxLength(50);
            entity.Property(e => e.MaxHoursPerYear).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.Remark).HasMaxLength(250);
        });

        modelBuilder.Entity<Overtimerequest>(entity =>
        {
            entity.HasKey(e => e.Otid).HasName("PK__overtime__A934AF24661AC4B5");

            entity.ToTable("overtimerequest");

            entity.Property(e => e.Otid)
                .HasMaxLength(50)
                .HasColumnName("OTID");
            entity.Property(e => e.ApproverId)
                .HasMaxLength(50)
                .HasColumnName("ApproverID");
            entity.Property(e => e.EmpId)
                .HasMaxLength(50)
                .HasColumnName("EmpID");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.Hours).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.Otdate).HasColumnName("OTDate");
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Approver).WithMany(p => p.OvertimerequestApprovers)
                .HasForeignKey(d => d.ApproverId)
                .HasConstraintName("fk_overtimerequest_employee_1");

            entity.HasOne(d => d.Emp).WithMany(p => p.OvertimerequestEmps)
                .HasForeignKey(d => d.EmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_overtimerequest_employee");
        });

        modelBuilder.Entity<SysAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__SysAccou__349DA5869E2FAFCD");

            entity.ToTable("SysAccount");

            entity.Property(e => e.AccountId)
                .HasMaxLength(50)
                .HasColumnName("AccountID");
            entity.Property(e => e.Account).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.EmpId)
                .HasMaxLength(50)
                .HasColumnName("EmpID");
            entity.Property(e => e.LastLogin).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(250);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Emp).WithMany(p => p.SysAccounts)
                .HasForeignKey(d => d.EmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SysAccount_Employee");
        });

        modelBuilder.Entity<SysLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__SysLog__5E5499A867433BDC");

            entity.ToTable("SysLog");

            entity.Property(e => e.LogId)
                .HasMaxLength(50)
                .HasColumnName("LogID");
            entity.Property(e => e.Action).HasMaxLength(250);
            entity.Property(e => e.Details).HasMaxLength(250);
            entity.Property(e => e.EmpId)
                .HasMaxLength(50)
                .HasColumnName("EmpID");
            entity.Property(e => e.Ip)
                .HasMaxLength(50)
                .HasColumnName("IP");
            entity.Property(e => e.LogTime).HasColumnType("datetime");
            entity.Property(e => e.TargetId)
                .HasMaxLength(50)
                .HasColumnName("TargetID");

            entity.HasOne(d => d.Emp).WithMany(p => p.SysLogs)
                .HasForeignKey(d => d.EmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_syslog_employee");
        });

        modelBuilder.Entity<SysNotification>(entity =>
        {
            entity.HasKey(e => e.NotifyId).HasName("PK__SysNotif__AD54A2DCF6BAF7B2");

            entity.ToTable("SysNotification");

            entity.Property(e => e.NotifyId)
                .HasMaxLength(50)
                .HasColumnName("NotifyID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Link).HasMaxLength(250);
            entity.Property(e => e.Message).HasMaxLength(250);
            entity.Property(e => e.ReceiverId)
                .HasMaxLength(50)
                .HasColumnName("ReceiverID");

            entity.HasOne(d => d.Receiver).WithMany(p => p.SysNotifications)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SysNotification_Employee");
        });

        modelBuilder.Entity<SysRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__SysRole__8AFACE3ACCF2ACAE");

            entity.ToTable("SysRole");

            entity.Property(e => e.RoleId)
                .HasMaxLength(50)
                .HasColumnName("RoleID");
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<SystemAttachment>(entity =>
        {
            entity.HasKey(e => e.AttachmentId).HasName("PK__SystemAt__442C64DEB9B443FE");

            entity.ToTable("SystemAttachment");

            entity.Property(e => e.AttachmentId)
                .HasMaxLength(50)
                .HasColumnName("AttachmentID");
            entity.Property(e => e.FileName).HasMaxLength(250);
            entity.Property(e => e.FilePath).HasMaxLength(500);
            entity.Property(e => e.ReferenceId)
                .HasMaxLength(50)
                .HasColumnName("ReferenceID");
            entity.Property(e => e.ReferenceType).HasMaxLength(50);
            entity.Property(e => e.UploadBy).HasMaxLength(50);
            entity.Property(e => e.UploadTime).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
