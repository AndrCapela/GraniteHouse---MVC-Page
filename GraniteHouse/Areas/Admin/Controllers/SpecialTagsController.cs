﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraniteHouse.Data;
using GraniteHouse.Models;
using Microsoft.AspNetCore.Mvc;

namespace GraniteHouse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialTagsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SpecialTagsController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            return View(_db.SpecialTags.ToList());
        }
        //Get Create Action Method
        public IActionResult Create()
        {
            return View();
        }

        //Post Create action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialTags specialTags)
        {
            if (ModelState.IsValid)
            {
                _db.Add(specialTags);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialTags);
        }

        //Get Edit Action Method
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var specialTags = await _db.SpecialTags.FindAsync(id);
            if (specialTags == null)
            {
                return NotFound();
            }

            return View(specialTags);
        }

        //Post Edit action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SpecialTags specialTags)
        {
            if (id != specialTags.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Update(specialTags);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialTags);
        }

        //Get Details Action Method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var specialTags = await _db.SpecialTags.FindAsync(id);
            if (specialTags == null)
            {
                return NotFound();
            }

            return View(specialTags);
        }

        //Get Delete Action Method
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var specialTags = await _db.SpecialTags.FindAsync(id);
            if (specialTags == null)
            {
                return NotFound();
            }

            return View(specialTags);
        }

        //Post Delete action Method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var specialTags = await _db.SpecialTags.FindAsync(id);
            _db.SpecialTags.Remove(specialTags);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}