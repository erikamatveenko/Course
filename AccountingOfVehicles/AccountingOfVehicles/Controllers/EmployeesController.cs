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

        public IActionResult Employees(int? currentParameterID, int page = 1)
        {
            int pageSize = 10;   // количество элементов на странице
            int currTitleID = currentParameterID ?? 0;

            List<TitleNameFilter> titleNames = _db.Titles.Select(b => new TitleNameFilter { TitleName = b.TitleName, Id = b.TitleID }).ToList();
            titleNames.Insert(0, new TitleNameFilter { TitleName = "Все", Id = 0 });

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
            TitleNameFilter tnf = titleNames.Where(c => c.Id == currTitleID).ToList()[0];
            if (currTitleID > 0)
            {
                employees = employees.Where(c => c.Title.TitleName == tnf.TitleName).ToList();
            }

            PageViewModel pageViewModel = new PageViewModel(employees.Count, page, pageSize, new CarsFilter());

            EmployeeViewModel employeeViewModel = new EmployeeViewModel { PageViewModel = pageViewModel, Employees = employees.Skip((page - 1) * pageSize).Take(pageSize).ToList(), TitleNames = titleNames, CurrentTitleName = tnf };
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
