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
    public class TitlesController : Controller
    {
        private UchetContext _db;

        public TitlesController(UchetContext db)
        {
            _db = db;
        }

        public IActionResult Titles(string titleName = "Все", int pageNumber = 1)
        {
            int pageSize = 10;   // количество элементов на странице

            List<string> titleNames = _db.Titles.Select(b => b.TitleName).ToList();
            titleNames.Insert(0, "Все");

            var titles = _db.Titles.OrderBy(s => s.TitleID).ToList();

            if (titleName != "Все")
            {
                titles = titles.Where(c => c.TitleName == titleName).ToList();
            }

            TitlesFilter titlesFilter = new TitlesFilter { titleName = titleName, TitleNames = titleNames };

            PageViewModel pageViewModel = new PageViewModel(titles.Count, pageNumber, pageSize, titlesFilter);

            TitleViewModel titleViewModel = new TitleViewModel { PageViewModel = pageViewModel,
                Titles = titles.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(), TitlesFilters = titlesFilter };
            return View(titleViewModel);
        }


        // GET: Titles/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var title = getTitleById(id);
            if (title == null)
            {
                return NotFound();
            }
            return View(title);
        }


        // POST: Titles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("TitleID,TitleName,TitleAllowance,TitleCharge,TitleRequirement")] Title title)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(title);
                    _db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TitleExists(title.TitleID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Titles");
            }
            return View(title);
        }


        // GET: Titles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Titles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("TitleID,TitleName,TitleAllowance,TitleCharge,TitleRequirement")] Title title)
        {
            if (ModelState.IsValid)
            {
                if (TitleExists(title.TitleID))
                {
                    return View("Message", "Уже существует звание с данным идентификатором!");

                }
                _db.Add(title);
                _db.SaveChanges();
                return RedirectToAction("Titles");
            }
            
            return View(title);
        }


        // GET: Titles/More/5
        public IActionResult More(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var title = getTitleById(id);

            if (title == null)
            {
                return NotFound();
            }

            return View(title);
        }



        // GET: Titles/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var title = getTitleById(id);

            if (title == null)
            {
                return NotFound();
            }

            return View(title);
        }

        // POST: Titles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int TitleID)
        {
            var title = _db.Titles.Where(c => c.TitleID == TitleID);
            _db.Titles.RemoveRange(title);
            var employee = _db.Employees.Where(c => c.TitleID == TitleID);
            _db.Employees.RemoveRange(employee);
            var stolenCar = _db.StolenCars.Where(c => c.Employee.TitleID == TitleID);
            _db.StolenCars.RemoveRange(stolenCar);
            _db.SaveChanges();
            return RedirectToAction("TItles");
        }

        private bool TitleExists(int id)
        {
            return _db.Titles.Any(e => e.TitleID == id);
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private Title getTitleById(int? id)
        {
            return _db.Titles.Where(c => c.TitleID == id).ToList()[0];
        }
    }
}
