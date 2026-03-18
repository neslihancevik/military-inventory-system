using System;
using System.Collections.Generic;

namespace MilitaryInventory.Models;

public partial class Personnel
{
    public Guid PersonnelId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Rank { get; set; }

    public string? Unit { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
}
