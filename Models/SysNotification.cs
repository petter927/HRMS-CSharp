using System;
using System.Collections.Generic;

namespace Skygate_AspNet_MVC.Models;

public partial class SysNotification
{
    public string NotifyId { get; set; } = null!;

    public string ReceiverId { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string? Link { get; set; }

    public bool IsRead { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Employee Receiver { get; set; } = null!;
}
