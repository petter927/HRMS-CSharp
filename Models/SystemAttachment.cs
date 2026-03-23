using System;
using System.Collections.Generic;

namespace Skygate_AspNet_MVC.Models;

public partial class SystemAttachment
{
    public string AttachmentId { get; set; } = null!;

    public string? ReferenceType { get; set; }

    public string? ReferenceId { get; set; }

    public string? FileName { get; set; }

    public string? FilePath { get; set; }

    public long? FileSize { get; set; }

    public DateTime? UploadTime { get; set; }

    public string? UploadBy { get; set; }
}
