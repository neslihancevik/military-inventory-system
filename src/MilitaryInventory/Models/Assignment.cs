using MilitaryInventory.Models;

public partial class Assignment
{
    public Guid AssignmentId { get; set; }
    public Guid EquipmentId { get; set; }
    public Guid PersonnelId { get; set; }

    // AssignedBy alanının sonuna ? koyarak boş geçilebilir yapıyoruz
    public Guid? AssignedBy { get; set; }

    public DateTime? AssignedDate { get; set; }
    public DateTime? ReturnedDate { get; set; }
    public bool? IsActive { get; set; }

    // Bunların sonuna ? eklemeyi unutma
    public virtual Equipment? Equipment { get; set; }
    public virtual Personnel? Personnel { get; set; }
    public virtual User? AssignedByNavigation { get; set; }
}