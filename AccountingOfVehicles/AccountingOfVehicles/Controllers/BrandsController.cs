using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AccountingOfVehicles.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AccountingOfVehicles.ViewModels;
using AccountingOfVehicles.Data;

namespace AccountingOfVehicles.Controllers
{
    public class BrandsController : Controller
    {
        private UchetContext _db;

        public BrandsController(UchetContext db)
        {
            _db = db;
        }
            
        public IActionResult Brands(int? currentParameterID, int page = 1)
        {
            int pageSize = 10;   // количество элементов на странице
            int currBrandID = currentParameterID ?? 0;

            List<BrandCountryFilter> brandCountries = _db.Brands.Select(b => new BrandCountryFilter { BrandCountry = b.BrandCountry, Id = b.BrandID }).ToList();
            brandCountries.Insert(0, new BrandCountryFilter { BrandCountry = "Все", Id = 0 });

            var brands = _db.Brands.OrderBy(s => s.BrandID).ToList();
            BrandCountryFilter brcf = brandCountries.Where(c => c.Id == currBrandID).ToList()[0];
            if (currBrandID > 0)
            {
                brands = brands.Where(c => c.BrandCountry == brcf.BrandCountry).ToList();
            }

            PageViewModel pageViewModel = new PageViewModel(brands.Count, page, pageSize, currBrandID);

            BrandViewModel brandViewModel = new BrandViewModel { PageViewModel = pageViewModel, Brands = brands.Skip((page - 1) * pageSize).Take(pageSize).ToList(), BrandCountries = brandCountries, CurrentBrandCountry = brcf };
            return View(brandViewModel);
        }

        // GET: Brands/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = getBrandById(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }


        // POST: Brands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("BrandID,BrandName," +
            "BrandCompany,BrandCountry,BrandStartDate,BrandEndingDate," +
            "BrandCharacteristic,BrandCategory,BrandDescription")] Brand brand)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(brand);
                    _db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(brand.BrandID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Brands");
            }
            return View(brand);
        }


        // GET: Brands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Brands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("BrandID,BrandName," +
            "BrandCompany,BrandCountry,BrandStartDate,BrandEndingDate," +
            "BrandCharacteristic,BrandCategory,BrandDescription")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                if (BrandExists(brand.BrandID))
                {
                    return View("Message", "Уже существует марка автомобиля с данным идентификатором!");
                }
                _db.Add(brand);
                _db.SaveChanges();
                return RedirectToAction("Brands");
            }
            
            return View(brand);
        }


        // GET: Brands/Details/5
        public IActionResult More(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = getBrandById(id);

            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }



        // GET: Brands/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = getBrandById(id);

            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int BrandID)
        {
            var brand = _db.Brands.Where(c => c.BrandID == BrandID);
            _db.Brands.RemoveRange(brand);
            var car = _db.Cars.Where(c => c.BrandID == BrandID);
            _db.Cars.RemoveRange(car);
            _db.SaveChanges();
            return RedirectToAction("Brands");
        }

        private bool BrandExists(int id)
        {
            return _db.Brands.Any(e => e.BrandID == id);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private Brand getBrandById(int? id)
        {
            return _db.Brands.Where(e => e.BrandID == id).ToList()[0];
        }
    }
}
