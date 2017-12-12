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
    public class EmployeesController : Controller
    {
        private UchetContext _db;

        public EmployeesController(UchetContext db)
        {
            _db = db;
        }

        public IActionResult Employees(string titleName = "Все", int pageNumber = 1)
        {
            int pageSize = 10;   // количество элементов на странице

            List<string> titleNames = _db.Titles.Select(b => b.TitleName).ToList();
            titleNames.Insert(0, "Все");

            List<Employee> employees = _db.Employees
                .Select(t => new Employee
                {
                    EmployeeID = t.EmployeeID,
                    TitleID = t.TitleID,
                    EmployeeName = t.EmployeeName,
                    EmployeeBirthDate = t.EmployeeBirthDate,
                    Title = t.Title
                }).OrderBy(s => s.EmployeeID)
                .ToList();

            if (titleName != "Все")
            {
                employees = employees.Where(c => c.Title.TitleName == titleName).ToList();
            }

            TitlesFilter titlesFilter = new TitlesFilter { titleName = titleName, TitleNames = titleNames };


            PageViewModel pageViewModel = new PageViewModel(employees.Count, pageNumber, pageSize, titlesFilter);

            EmployeeViewModel employeeViewModel = new EmployeeViewModel { PageViewModel = pageViewModel,
                Employees = employees.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(), TitlesFilters = titlesFilter };
            return View(employeeViewModel);
        }


        // GET: Employees/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = getEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["TitleID"] = new SelectList(_db.Titles, "TitleID", "TitleName", employee.TitleID);
            return View(employee);
        }


        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("EmployeeID,TitleID,EmployeeName,EmployeeBirthDate")] Employee employee)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(employee);
                    _db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Employees");
            }
            ViewData["TitleID"] = new SelectList(_db.Titles, "TitleID", "TitleName", employee.TitleID);
            return View(employee);
        }


        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["TitleID"] = new SelectList(_db.Titles, "TitleID", "TitleName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("EmployeeID,TitleID,EmployeeName,EmployeeBirthDate")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (EmployeeExists(employee.EmployeeID))
                {
                    return View("Message", "Уже существует сотрудник с данным идентификатором!");

                }
                _db.Add(employee);
                _db.SaveChanges();
                return RedirectToAction("Employees");
            }

            ViewData["TitleID"] = new SelectList(_db.Titles, "TitleID", "TitleName", employee.TitleID);
            return View(employee);
        }


        // GET: Employees/Details/5
        public IActionResult More(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = getEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }



        // GET: Employees/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = getEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int EmployeeID)
        {
            var employee = _db.Employees.Where(c => c.EmployeeID == EmployeeID);
            _db.Employees.RemoveRange(employee);
            var stolenCars = _db.StolenCars.Where(c => c.EmployeeID == EmployeeID);
            _db.StolenCars.RemoveRange(stolenCars);
            _db.SaveChanges();
            return RedirectToAction("Employees");
        }

        private bool EmployeeExists(int id)
        {
            return _db.Employees.Any(e => e.EmployeeID == id);
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private Employee getEmployeeById(int? id)
        {
            return _db.Employees.Select(t => new Employee
            {
                EmployeeID = t.EmployeeID,
                TitleID = t.TitleID,
                EmployeeName = t.EmployeeName,
                EmployeeBirthDate = t.EmployeeBirthDate,
                Title = t.Title
            }).Where(c => c.EmployeeID == id).ToList()[0];
        }
    }
}
