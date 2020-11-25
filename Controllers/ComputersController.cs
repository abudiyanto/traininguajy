using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrainingUAJY.Migrations;
using TrainingUAJY.Models;

namespace TrainingUAJY.Controllers
{
    public class ComputersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Computers
        public async Task<ActionResult> Index()
        {
            var computers = await db.Computers.Include("Processor").Include("Brand").ToListAsync();
            return View(computers);
        }
        
        // GET: Computers/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Computer computer = await db.Computers.FindAsync(id);
            if (computer == null)
            {
                return HttpNotFound();
            }
            return View(computer);
        }

        // GET: Computers/Create
        public ActionResult Create()
        {
            var brands = db.Brands.Select(i => new SelectListItem()
            {
                Text = i.Title,
                Value = i.IdBrand,
                Selected = false
            }).ToArray();
            ViewBag.Brands = brands;

            var processors = db.Processors.Select(i => new SelectListItem()
            {
                Text = i.Title,
                Value = i.IdProcessor,
                Selected = false
            }).ToArray();
            ViewBag.Processors = processors;

            return View();
        }

        // POST: Computers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ViewModels.Computer addComputer)
        {
            if (ModelState.IsValid)
            {
                var brand = await db.Brands.Where(x => x.IdBrand == addComputer.Brand).SingleOrDefaultAsync();
                var processor = await db.Processors.Where(x => x.IdProcessor == addComputer.Processor).SingleOrDefaultAsync();
                var computer = new Models.Computer()
                {
                    SKU = addComputer.SKU,
                    Title = addComputer.Title,
                    Description = addComputer.Description,
                    Brand = brand,
                    Processor = processor,
                    ProductionYear = addComputer.ProductionYear,
                    ScreenSize = addComputer.ScreenSize,
                    RAM = addComputer.RAM,
                    Storage = addComputer.Storage,
                    Stock = addComputer.Stock,
                    Price = addComputer.Price
                };
                db.Computers.Add(computer);
                var result = await db.SaveChangesAsync();
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View("Error");
        }

        // GET: Computers/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Computer computer = await db.Computers.FindAsync(id);
            if (computer == null)
            {
                return HttpNotFound();
            }
            return View(computer);
        }

        // POST: Computers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ViewModels.Computer edit)
        {
            if (ModelState.IsValid)
            {
                var computer = await db.Computers.Where(x => x.SKU == edit.SKU).SingleOrDefaultAsync();
                if (computer != null)
                {
                    computer.Title = edit.Title;
                    computer.Description = edit.Description;
                    computer.Stock = edit.Stock;
                    computer.Price = edit.Price;
                    db.Entry(computer).State = EntityState.Modified;
                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                    {
                        return RedirectToAction("Index");
                    }
                  
                }
               
            }
            return View("HttpNotFound");
        }

        // GET: Computers/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Computer computer = await db.Computers.FindAsync(id);
            if (computer == null)
            {
                return HttpNotFound();
            }
            return View(computer);
        }

        // POST: Computers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Computer computer = await db.Computers.FindAsync(id);
            db.Computers.Remove(computer);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
