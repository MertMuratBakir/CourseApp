using System.Threading.Tasks;
using CourseApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace CourseApp.Controllers
{
    public class KursKayitController : Controller
    {
        private readonly DataContext _context;
        public KursKayitController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var kursKayitlari = await
            _context.Kurskayitlari
            .Include(x => x.Ogrenci)
            .Include(x => x.Kurs)
            .ToListAsync();
            return View(kursKayitlari);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Ogrenciler = new SelectList(await _context.Ogrenciler.ToListAsync(), "OgrenciId", "AdSoyad");
            ViewBag.Kurslar = new SelectList(await _context.Kurslar.ToListAsync(), "KursId", "Baslik");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KursKayit model)
        {
            _context.Kurskayitlari.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kursKayit = await _context.Kurskayitlari
                .Include(x => x.Ogrenci)
                .Include(x => x.Kurs)
                .FirstOrDefaultAsync(k => k.KayitId == id);

            if (kursKayit == null)
            {
                return NotFound();
            }

            ViewBag.Ogrenciler = new SelectList(await _context.Ogrenciler.ToListAsync(), "OgrenciId", "AdSoyad", kursKayit.OgrenciId);
            ViewBag.Kurslar = new SelectList(await _context.Kurslar.ToListAsync(), "KursId", "Baslik", kursKayit.KursId);

            return View(kursKayit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KursKayit Model)
        {
            if (id != Model.KayitId)
            {
                return NotFound();
            }
            else
            {
                _context.Update(Model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var KursKayit = await _context.Kurskayitlari
                .Include(x => x.Ogrenci)
                .Include(x => x.Kurs)
                .FirstOrDefaultAsync(x => x.KayitId == id);

            if (KursKayit == null)
            {
                return NotFound();
            }

            return View(KursKayit);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kursKayit = await _context.Kurskayitlari.FindAsync(id);

            if (kursKayit == null)
            {
                return NotFound();
            }

            _context.Kurskayitlari.Remove(kursKayit);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}




