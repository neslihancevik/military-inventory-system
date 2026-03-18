using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilitaryInventory.Data;
using MilitaryInventory.Models;

namespace MilitaryInventory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssignmentController : ControllerBase
    {
        private readonly MilitaryDbContext _context;

        public AssignmentController(MilitaryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Assignments
                .Include(a => a.Personnel)
                .Include(a => a.Equipment)
                .Include(a => a.AssignedByNavigation)
                .ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Assignment assignment)
        {
            if (assignment.AssignmentId == Guid.Empty)
                assignment.AssignmentId = Guid.NewGuid();

            assignment.AssignedDate = DateTime.Now;

            // 1. Zimmet Kaydını Ekle
            _context.Assignments.Add(assignment);

            // 2. AuditLog (İşlem Geçmişi) Kaydı Oluştur
            var log = new AuditLog
            {
                Action = "CREATE",
                TableName = "Assignments",
                ActionDate = DateTime.Now,
                Details = $"Ekipman ({assignment.EquipmentId}) personele ({assignment.PersonnelId}) zimmetlendi."
            };
            _context.AuditLogs.Add(log);

            // 3. Her iki değişikliği tek seferde kaydet
            await _context.SaveChangesAsync();

            return Ok(new { message = "Zimmet işlemi başarıyla kaydedildi ve loglandı.", logId = log.LogId });
        }
    }
}