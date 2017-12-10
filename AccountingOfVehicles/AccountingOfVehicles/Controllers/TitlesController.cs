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
    public class TitlesController : Controller
    {
        private UchetContext _db;

        public TitlesController(UchetContext db)
        {
            _db = db;
        }

        public IActionResult Titles(int? currentParameterID, int page = 1)
        {
            int pageSize = 10;   // количество элементов на странице
            int currTitleID = currentParameterID ?? 0;

            List<TitleAllowanceFilter> titleAllowances = _db.Titles.Select(b => new TitleAllowanceFilter { TitleAllowance = b.TitleAllowance.ToString(), Id = b.TitleID }).ToList();
            titleAllowances.Insert(0, new TitleAllowanceFilter { TitleAllowance = "Все", Id = 0 });

            var titles = _db.Titles.OrderBy(s => s.TitleID).ToList();

            TitleAllowanceFilter taf = titleAllowances.Where(c => c.Id == currTitleID).ToList()[0];
            if (currTitleID > 0)
            {
                titles = titles.Where(c => c.TitleID == currTitleID).ToList();
            }

            PageViewModel pageViewModel = new PageViewModel(titles.Count, page, pageSize, currTitleID);

            TitleViewModel titleViewModel = new TitleViewModel { PageViewModel = pageViewModel, Titles = titles.Skip((page - 1) * pageSize).Take(pageSize).ToList(), TitleAllowances = titleAllowances, CurrentTitleAllowance = taf };
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
