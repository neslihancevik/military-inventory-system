using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilitaryInventory.Data;
using MilitaryInventory.Models;

namespace MilitaryInventory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipmentController : ControllerBase
    {
        private readonly MilitaryDbContext _context;

        public EquipmentController(MilitaryDbContext context)
        {
            _context = context;
        }

        // 1. Tüm Ekipmanları Listele (Tipiyle Birlikte)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var equipments = await _context.Equipment
                .Include(e => e.EquipmentType)
                .ToListAsync();
            return Ok(equipments);
        }

        // 2. Yeni Ekipman Ekle
        [HttpPost]
        public async Task<IActionResult> Create(Equipment equipment)
        {
            if (equipment.EquipmentId == Guid.Empty)
                equipment.EquipmentId = Guid.NewGuid();

            _context.Equipment.Add(equipment);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Ekipman başarıyla eklendi!", id = equipment.EquipmentId });
        }

        // 3. Ekipman Sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var equipment = await _context.Equipment.FindAsync(id);
            if (equipment == null) return NotFound("Ekipman bulunamadı.");

            _context.Equipment.Remove(equipment);
            await _context.SaveChangesAsync();
            return Ok("Ekipman silindi.");
        }

        // --- YENİ EKLENEN RAPORLAMA METOTLARI ---

        // 4. Duruma göre ekipmanları getir (Örn: Boşta olanlar / Zimmettekiler)
        // URL: api/Equipment/status/true
        [HttpGet("status/{isAvailable}")]
        public async Task<IActionResult> GetByStatus(bool isAvailable)
        {
            var equipments = await _context.Equipment
                .Include(e => e.EquipmentType)
                .Where(e => e.IsAvailable == isAvailable)
                .ToListAsync();
            return Ok(equipments);
        }

        // 5. Ekipman tipine göre getir (Örn: Sadece Telsizler)
        // URL: api/Equipment/type/3
        [HttpGet("type/{typeId}")]
        public async Task<IActionResult> GetByType(int typeId)
        {
            var equipments = await _context.Equipment
                .Include(e => e.EquipmentType)
                .Where(e => e.EquipmentTypeId == typeId)
                .ToListAsync();
            return Ok(equipments);
        }

        // 6. Seri numarası ile arama yap (Kısmi eşleşme destekler)
        // URL: api/Equipment/search/ASEL
        [HttpGet("search/{serial}")]
        public async Task<IActionResult> SearchBySerial(string serial)
        {
            var equipments = await _context.Equipment
                .Include(e => e.EquipmentType)
                .Where(e => e.SerialNumber.Contains(serial))
                .ToListAsync();

            return Ok(equipments);
        }
    }
}