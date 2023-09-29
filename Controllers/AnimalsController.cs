using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using PeitsShop.DataContext;
using PeitsShop.Models;

namespace PeitsShop.Controllers
{
    public class AnimalsController : Controller 
    {
        private readonly Data _context;

        public AnimalsController(Data context)
        {
            _context = context;
        }

        // GET: Animals
        public async Task<IActionResult> Index()
        {
            var Data = await _context.Animals.Include(a => a.Category).ToListAsync();
            var CommentData = await _context.Animals.Include(a => a.Comments).ToListAsync();
            Data = Data.Where(a => a.Comments.Count > 0)
                .OrderByDescending(a => a.Comments.Count)
                .Take(2)
                .ToList();

            return View(Data);
        }

        public IActionResult Catalog(int categoryId = 0)
        {
            var categories = _context.Categories.ToList();
            var animals = _context.Animals.ToList();

            if (categoryId != 0)
            {
                animals = animals.Where(a => a.CategoryId == categoryId).ToList();
            }

            ViewBag.Categories = categories;
            return View(animals);
        }

        public IActionResult Animal(int? categoryId)
        {
            var categories = _context.Categories.ToList();
            if(categoryId.HasValue)
            {
                categories = categories.Where(c =>  c.CategoryId == categoryId).ToList();
            }
            ViewBag.Categories = categories;
            return View(categories);

        }

        public async Task<IActionResult> Administrator(int categoryId = 0)
        {
            var categories = _context.Categories.ToList();
            var animals = _context.Animals.ToList();

            if (categoryId != 0)
            {
                animals = animals.Where(a => a.CategoryId == categoryId).ToList();
            }

            ViewBag.Categories = categories;
            return View(animals);
        }

        // GET: Animals/Details/5
        public async Task<IActionResult> Details(int? AnimalId)
        {
            if (AnimalId == null || _context.Animals == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals
                .Include(a => a.Category).Include(a => a.Comments)
                .FirstOrDefaultAsync(m => m.AnimalId == AnimalId);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        public async Task<IActionResult> AddComment(int AnimalId, string commentText)
        {
            if (AnimalId == null || _context.Animals == null)
            {
                return NotFound();
            }

            if (!commentText.IsNullOrEmpty())
            {
                var newComment = new Comment()
                {
                    AnimalId = AnimalId,
                    CommentContent = commentText
                };

                _context.Comments.Add(newComment);
                await _context.SaveChangesAsync();
            }
            

            return RedirectToAction("Details", new { AnimalId });
        }

        // GET: Animals/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Animals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimalId,Name,Age,Image,Description,CategoryId")] Animal animal, IFormFile Image)
        {
            var categories = _context.Categories.ToList();
            ViewBag.Categories = categories;

            if (ModelState.IsValid)
            {
                if (Image != null && Image.Length > 0)
                {
                    var uniqueFileName = Image.FileName;
                    var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images");
                    var filePath = Path.Combine(uploadDir, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Image.CopyTo(stream);
                    }

                    animal.Image = "/Images/" + uniqueFileName;
                }

                _context.Animals.Add(animal);
                _context.SaveChanges();

                /*                ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", animal.CategoryId);               
                */
                return RedirectToAction("Administrator");
            }

            return View(animal);
        }

        // GET: Animals/Edit/5
        public async Task<IActionResult> Edit(int? AnimalId)
        {

            if (AnimalId == null || _context.Animals == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals.FindAsync(AnimalId);
            if (animal == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", animal.CategoryId);
            return View(animal);
        }

        // POST: Animals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnimalId,Name,Age,Image,Description,CategoryId")] Animal animal, IFormFile Image)
        {
            var categories = _context.Categories.ToList();
            ViewBag.Categories = categories;

            if (id != animal.AnimalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldAnimal = await _context.Animals.FindAsync(id);
                    oldAnimal.Name = animal.Name;
                    oldAnimal.Age = animal.Age;
                    oldAnimal.Description = animal.Description;
                    oldAnimal.CategoryId = animal.CategoryId;

                    if (Image != null && Image.Length > 0)
                    {
                        var uniqueFileName = Image.FileName;
                        var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images");
                        var filePath = Path.Combine(uploadDir, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            Image.CopyTo(stream);
                        }

                        animal.Image = "/Images/" + uniqueFileName;
                        oldAnimal.Image = animal.Image;

                    }

                    _context.Update(oldAnimal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.AnimalId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Administrator");
            }
            return View(animal.AnimalId);
        }

        // GET: Animals/Delete/5
        public async Task<IActionResult> Delete(int? AnimalId)
        {
            if (AnimalId == null || _context.Animals == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.AnimalId == AnimalId);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Animals == null)
            {
                return Problem("Entity set 'Data.Animals'  is null.");
            }
            var animal = await _context.Animals.FindAsync(id);
            if (animal != null)
            {
                _context.Animals.Remove(animal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Administrator");
        }

        private bool AnimalExists(int id)
        {
            return (_context.Animals?.Any(e => e.AnimalId == id)).GetValueOrDefault();
        }
    }
}
