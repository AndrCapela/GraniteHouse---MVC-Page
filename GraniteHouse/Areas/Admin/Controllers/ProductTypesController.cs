using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraniteHouse.Data;
using GraniteHouse.Models;
using Microsoft.AspNetCore.Mvc;

namespace GraniteHouse.Areas.Admin.Controllers
{   [Area("Admin")]
    public class ProductTypesController : Controller
    {

        private readonly ApplicationDbContext _db;
        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.ProductTypes.ToList());
        }

        //Get Create Action Method
        public IActionResult Create()
        {
            return View();
        }

        //Post Create action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes producttypes)
        {
            if (ModelState.IsValid)
            {
                _db.Add(producttypes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producttypes);
        }

        //Get Edit Action Method
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ProductType = await _db.ProductTypes.FindAsync(id);
            if(ProductType == null)
            {
                return NotFound();
            }

            return View(ProductType);
        }

        //Post Edit action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductTypes producttypes)
        {
            if (id != producttypes.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Update(producttypes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producttypes);
        }
    }
}