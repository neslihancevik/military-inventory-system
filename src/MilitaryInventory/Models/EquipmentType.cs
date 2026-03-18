using System;
using System.Collections.Generic;

namespace MilitaryInventory.Models;

public partial class EquipmentType
{
    public int EquipmentTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();
}
