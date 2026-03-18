using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilitaryInventory.Data;
using MilitaryInventory.Models;

namespace MilitaryInventory.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Bu satır Swagger'da görünmesi için şarttır
    public class AuditLogController : ControllerBase
    {
        private readonly MilitaryDbContext _context;

        public AuditLogController(MilitaryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLogs()
        {
            // Logları en yeni tarihten en eskiye doğru sıralayarak getirir
            var logs = await _context.AuditLogs.OrderByDescending(x => x.ActionDate).ToListAsync();
            return Ok(logs);
        }
    }
}