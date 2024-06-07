using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Data;
using Pizzeria.Models;

namespace Pizzeria.Controllers
{
    public class ToppingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ToppingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Toppings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Toppings.ToListAsync());
        }

        // GET: Toppings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Toppings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Topping topping)
        {
            if(await _context.Toppings.AnyAsync(t => t.Name == topping.Name)){
                ModelState.AddModelError("Name", "Topping with this name already exists.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(topping);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(topping);
        }

        // GET: Toppings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            

            var topping = await _context.Toppings.FindAsync(id);
            if (topping == null)
            {
                return NotFound();
            }
            return View(topping);
        }

        // POST: Toppings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Topping topping)
        {
            if (id != topping.Id)
            {
                return NotFound();
            }

            if (await _context.Toppings.AnyAsync(t => t.Id != topping.Id && t.Name == topping.Name))
            {
                ModelState.AddModelError("Name", "Topping with this name already exists.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToppingExists(topping.Id))
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
            return View(topping);
        }

        // GET: Toppings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topping = await _context.Toppings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topping == null)
            {
                return NotFound();
            }

            return View(topping);
        }

        // POST: Toppings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var topping = await _context.Toppings.Include(t => t.Pizzas).FirstOrDefaultAsync(t => t.Id == id);
            if (topping != null)
            {
                if (topping.Pizzas!.Any()) // Sprawdzenie, czy istnieją pizze z tym dodatkiem
                {
                    ModelState.AddModelError("", "Cannot delete topping because it is used in existing pizzas.");
                    return View(topping);
                }

                _context.Toppings.Remove(topping);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToppingExists(int id)
        {
            return _context.Toppings.Any(e => e.Id == id);
        }
    }
}
