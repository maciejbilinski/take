using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Data;
using Pizzeria.Models;

namespace Pizzeria.Controllers
{
    public class PizzasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PizzasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pizzas
        public async Task<IActionResult> Index(string? searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var pizzas = _context.Pizzas.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                pizzas = pizzas.Where(p => p.Name.Contains(searchString));
            }

            return View(await pizzas.ToListAsync());
        }

        // GET: Pizzas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizzas.Include(p => p.Toppings)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        // GET: Pizzas/Create
        public IActionResult Create()
        {
            ViewBag.Toppings = _context.Toppings.ToList();
            return View();
        }

        // POST: Pizzas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,SmallPrice,MediumPrice,LargePrice,CrustType")] Pizza pizza, int[] toppings)
        {
            if (ModelState.IsValid)
            {
                if (await _context.Pizzas.AnyAsync(p => p.Name == pizza.Name))
                {
                    ModelState.AddModelError("Name", "Pizza with this name already exists.");
                }

                if (toppings != null)
                {
                    pizza.Toppings = _context.Toppings.Where(t => toppings.Contains(t.Id)).ToList();
                }

                _context.Add(pizza);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }


            return View(pizza);
        }

        // GET: Pizzas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizzas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        // POST: Pizzas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pizza = await _context.Pizzas.FindAsync(id);
            if (pizza != null)
            {
                _context.Pizzas.Remove(pizza);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PizzaExists(int id)
        {
            return _context.Pizzas.Any(e => e.Id == id);
        }
    }
}
