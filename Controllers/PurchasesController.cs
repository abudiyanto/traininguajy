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
    public class PurchasesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Purchases
        public async Task<ActionResult> Index()
        {
            var transaction = await db.Transactions.Include("Computer").Where(x => x.Type == TransactionType.Debet && x.Amount > 0).ToListAsync();

            var purchases = await db.Transactions.Include("Computer").Where(x => x.Type == TransactionType.Credit).ToListAsync();
            ViewBag.Purchases = purchases;

            return View(transaction);
        }
        public async Task<ActionResult> Create(string idTransaction)
        {
            var transaction = await db.Transactions.Include("Computer").Where(x => x.IdTransaction == idTransaction).SingleOrDefaultAsync();
            return View(transaction);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ViewModels.Transaction buy)
        {
            if (ModelState.IsValid)
            {
                var currentUTCTime = DateTimeOffset.UtcNow;
                var transaction = await db.Transactions.Include("Computer").Where(x => x.IdTransaction == buy.IdTransaction).SingleOrDefaultAsync();
                var stock = transaction.Amount - buy.Amount;
                var total = transaction.Computer.Price * buy.Amount;
                var purchase = new Models.Transaction()
                {
                    IdTransaction = Guid.NewGuid().ToString(),
                    Computer = transaction.Computer,
                    Notes = buy.Notes,
                    Created = currentUTCTime,
                    Type = TransactionType.Credit,
                    Amount = buy.Amount,
                    TotalTransaction = total
                };
                db.Transactions.Add(purchase);
                var result = await db.SaveChangesAsync();
                if (result > 0)
                {
                    var totalBuyers = transaction.TotalTransaction + total;
                    transaction.TotalTransaction = totalBuyers;
                    transaction.Amount = stock;
                    db.Entry(transaction).State = EntityState.Modified;
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