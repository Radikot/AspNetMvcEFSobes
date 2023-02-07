using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspNetMvcEFSobes.Data;
using AspNetMvcEFSobes.Models;

namespace AspNetMvcEFSobes.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationContext _context;

        public AccountController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Accounts
        public async Task<IActionResult> Index(string searchString)
        {

            var accounts = _context.Accounts.Select(a => a);
            if (!String.IsNullOrEmpty(searchString))
            { accounts = accounts.Where(a => a.Id.ToString().StartsWith(searchString)); }
                return accounts != null ? 
                          View(await accounts.ToListAsync()) :
                          Problem("Entity set 'ApplicationContext.Accounts'  is null.");
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PersonId,MoneyRUB,isJuridicalPerson")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PersonId,MoneyRUB,isJuridicalPerson")] Account account)
        {
            if (id != account.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]

        public IActionResult DeleteConfirmed(int id)
        {
            /*if (_context.Accounts == null)
            {
                return Problem("Entity set 'ApplicationContext.Accounts'  is null.");
            }*/
            var account = _context.Accounts.Find(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
            }
            
            _context.SaveChanges();
            return Ok();
            /*RedirectToAction(nameof(Index));*/
        }
        [HttpPost]
        public IActionResult Test(int id)
        {
            return Ok();
        }
        private bool AccountExists(int id)
        {
          return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private void Initialize()
        {
            _context.Database.EnsureCreated();


            var accounts = new Account[]
            {
                new Account { PersonId = 1, MoneyRUB = 15M, isJuridicalPerson = true },
                new Account { PersonId = 1, MoneyRUB = 15M, isJuridicalPerson = true },
                new Account { PersonId = 2, MoneyRUB = 15M, isJuridicalPerson = false },
                new Account { PersonId = 3, MoneyRUB = 15M, isJuridicalPerson = false },
                new Account { PersonId = 3, MoneyRUB = 15M, isJuridicalPerson = false },
                new Account { PersonId = 4, MoneyRUB = 15M, isJuridicalPerson = true },
                new Account { PersonId = 5, MoneyRUB = 15M, isJuridicalPerson = true }
            };
            _context.Accounts.AddRange(accounts);

            var operations = new Operation[]
            {
                new Operation { isIncome = false, BalanceChange = -500M, DateTime = DateTime.Now, AccountId = 1 },
                new Operation { isIncome = false, BalanceChange = -200M, DateTime = DateTime.Now, AccountId = 2 },
                new Operation { isIncome = true, BalanceChange = 400M, DateTime = DateTime.Now, AccountId = 2 },
                new Operation { isIncome = true, BalanceChange = 700M, DateTime = DateTime.Now, AccountId = 3 },
                new Operation { isIncome = true, BalanceChange = 100M, DateTime = DateTime.Now, AccountId = 4 },
                new Operation { isIncome = true, BalanceChange = 88M, DateTime = DateTime.Now, AccountId = 5 },
                new Operation { isIncome = true, BalanceChange = 22M, DateTime = DateTime.Now, AccountId = 6 },
                new Operation { isIncome = false, BalanceChange = -1000M, DateTime = DateTime.Now, AccountId = 7 }
            };
            _context.Operations.AddRange(operations);
            _context.SaveChanges();
        }
    }
}
