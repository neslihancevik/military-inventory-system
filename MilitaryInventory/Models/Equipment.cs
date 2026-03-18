using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MilitaryInventory.Models;

public partial class Equipment
{
    [Key] // Birincil anahtar olduğunu kesinleştirir
    public Guid EquipmentId { get; set; } = Guid.NewGuid(); // Yeni kayıtlarda otomatik ID oluşturur

    public string EquipmentName { get; set; } = null!;

    public string SerialNumber { get; set; } = null!;

    public int? EquipmentTypeId { get; set; }

    public bool? IsAvailable { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual EquipmentType? EquipmentType { get; set; }
}
