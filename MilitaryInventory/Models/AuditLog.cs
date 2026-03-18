using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // Bunu en üste ekleyin
namespace MilitaryInventory.Models;

public partial class AuditLog
{
    [Key]
    public int LogId { get; set; }

    public Guid? UserId { get; set; }

    public string? Action { get; set; }

    public string? TableName { get; set; }

    public string? RecordId { get; set; }

    public DateTime? ActionDate { get; set; }
    public string Details { get; set; } = null!;
    public string? Ipaddress { get; set; }
    public virtual User? User { get; set; } // Bu satırı AuditLog sınıfının içine ekle
}
