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
using AccountingOfVehicles.Models.Filters;

namespace AccountingOfVehicles.Controllers
{
    public class OwnersController : Controller
    {
        private UchetContext _db;

        public OwnersController(UchetContext db)
        {
            _db = db;
        }

        public IActionResult Owners(string brandName = "Все", string isNoDriver = "of", int pageNumber = 1)
        {
            int pageSize = 10;   // количество элементов на странице

            List<String> brandNames = _db.Brands.Select(b => b.BrandName).ToList();
            brandNames.Insert(0, "Все");

            List<Owner> owners = _db.Owners.OrderBy(s => s.OwnerID).ToList();

            if (brandName != "Все" && brandName != null)
            {
                owners = _db.Cars.Where(c => c.Brand.BrandName == brandName).Select(b => b.Owner).OrderBy(s => s.OwnerID).ToList();
            }

            if (isNoDriver == "on" && isNoDriver != null)
            {
                owners = owners.Where(c => c.OwnerNumberOfDriverLicense == null).ToList();
            }

            OwnersFilter ownersFilter = new OwnersFilter
            {
                brandName = brandName,         
                BrandNames = brandNames,
                isNoDriver = isNoDriver
            };


            PageViewModel pageViewModel = new PageViewModel(owners.Count, pageNumber, pageSize, ownersFilter);

            OwnerViewModel ownerViewModel = new OwnerViewModel { PageViewModel = pageViewModel,
                Owners = owners.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(),OwnersFilters = ownersFilter
            };
            return View(ownerViewModel);
        }


        // GET: Owners/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = getOwnerById(id);
            if (owner == null)
            {
                return NotFound();
            }
            return View(owner);
        }


        // POST: Owners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("OwnerID,OwnerName,OwnerBirthDate,OwnerAddress," +
            "OwnerPassport,OwnerNumberOfDriverLicense,OwnerLicenseDeliveryDate,OwnerLicenseValidityDate," +
            "OwnerCategory,OwnerMoreInformation")] Owner owner)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(owner);
                    _db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OwnerExists(owner.OwnerID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Owners");
            }
            return View(owner);
        }


        // GET: Owners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Owners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("OwnerID,OwnerName,OwnerBirthDate,OwnerAddress," +
            "OwnerPassport,OwnerNumberOfDriverLicense,OwnerLicenseDeliveryDate,OwnerLicenseValidityDate," +
            "OwnerCategory,OwnerMoreInformation")] Owner owner)
        {
            if (ModelState.IsValid)
            {
                if (OwnerExists(owner.OwnerID))
                {
                    return View("Message", "Уже существует владелец с данным идентификатором!");

                }
                _db.Add(owner);
                _db.SaveChanges();
                return RedirectToAction("Owners");
            }
            
            return View(owner);
        }


        // GET: Owners/More/5
        public IActionResult More(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = getOwnerById(id);

            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }



        // GET: Owners/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = getOwnerById(id);

            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // POST: Owners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int OwnerID)
        {
            var owner = _db.Owners.Where(c => c.OwnerID == OwnerID);
            _db.Owners.RemoveRange(owner);
            var car = _db.Cars.Where(c => c.OwnerID == OwnerID);
            _db.Cars.RemoveRange(car);
            _db.SaveChanges();
            return RedirectToAction("Owners");
        }

        private bool OwnerExists(int id)
        {
            return _db.Owners.Any(e => e.OwnerID == id);
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private Owner getOwnerById(int? id)
        {
            return  _db.Owners.Where(c => c.OwnerID == id).ToList()[0];
        }
    }
}
