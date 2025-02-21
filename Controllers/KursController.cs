using CourseApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseApp.Controllers
{
    public class KursController:Controller
    {
        private readonly DataContext _context;
        public KursController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var Kurslar = await _context.Kurslar.ToListAsync();
            return View(Kurslar);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Kurs model)
        {
            _context.Kurslar.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Kurs");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var kurs = await _context.Kurslar.FindAsync(id);

                if (kurs == null)
                {
                    return NotFound();
                }
                return View(kurs);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Kurs Model)
        {
            if (id != Model.KursId)
            {
                return NotFound();
            }
            else
            {
                _context.Update(Model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Kurs");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var Kurs = await _context.Kurslar.FindAsync(id);

                if (Kurs == null)
                {
                    return NotFound();
                }
                return View(Kurs);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var Kurs = await _context.Kurslar.FindAsync(id);
            if (Kurs == null)
            {
                return NotFound();
            }
            _context.Kurslar.Remove(Kurs);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Kurs");
        }


    } 
}