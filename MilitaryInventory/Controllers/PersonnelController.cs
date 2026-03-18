using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilitaryInventory.Data;
using MilitaryInventory.Models;

namespace MilitaryInventory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonnelController : ControllerBase
    {
        private readonly MilitaryDbContext _context;

        public PersonnelController(MilitaryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _context.Personnel.ToListAsync());

        [HttpPost]
        public async Task<IActionResult> Create(Personnel personnel)
        {
            if (personnel.PersonnelId == Guid.Empty) personnel.PersonnelId = Guid.NewGuid();
            _context.Personnel.Add(personnel);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Personel başarıyla eklendi!", id = personnel.PersonnelId });
        }
    }
}