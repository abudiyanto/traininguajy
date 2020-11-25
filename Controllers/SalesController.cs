using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TrainingUAJY.Models;

namespace TrainingUAJY.Controllers
{
    public class SalesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Sales
        public async Task<ActionResult> Index()
        {
            var transaction = await db.Transactions.Include("Computer").Where(x => x.Type == TransactionType.Debet).ToListAsync();
            return View(transaction);
        }
        public async Task<ActionResult> Create()
        {
            var computers = db.Computers.Where(x => x.Stock > 0).Select(i => new SelectListItem()
            {
                Text = i.Title,
                Value = i.SKU,
                Selected = false
            }).ToArray();
            ViewBag.Computers = computers;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ViewModels.Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                var currentUTCTime = DateTimeOffset.UtcNow;
                var computer = await db.Computers.Where(x => x.SKU == transaction.Computer).SingleOrDefaultAsync();
                var stock = computer.Stock - transaction.Amount;
                var sale = new Models.Transaction()
                {
                    IdTransaction = Guid.NewGuid().ToString(),
                    Computer = computer,
                    Notes = transaction.Notes,
                    Created = currentUTCTime,
                    Type = TransactionType.Debet,
                    Amount = transaction.Amount
                };
                db.Transactions.Add(sale);
                var result = await db.SaveChangesAsync();
                if (result > 0)
                {
                    computer.Stock = stock;
                    db.Entry(computer).State = EntityState.Modified;
                    var resultV2 = await db.SaveChangesAsync();
                    if (resultV2 > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View("Error");
        }
    }
}