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
using AccountingOfVehicles.Utils;

namespace AccountingOfVehicles.Controllers
{
    public class StolenCarsController : Controller
    {
        private UchetContext _db;

        public StolenCarsController(UchetContext db)
        {
            _db = db;
        }

        public IActionResult StolenCars(string brandName = "Все", string ownerName = "Все", string carNumberOfMotor = "Все",
            string startRegistrationDate = "", string endRegistrationDate = "", string isFind = "of", int pageNumber = 1)
        {
            int pageSize = 10;   // количество элементов на странице

            List<String> brandNames = _db.Brands.Select(b => b.BrandName).ToList();
            brandNames.Insert(0, "Все");

            List<String> ownerNames = _db.Owners.Select(b => b.OwnerName).ToList();
            ownerNames.Insert(0, "Все");

            List<String> carNumbersOfMotor = _db.Cars.Select(b => b.CarNumberOfMotor).ToList();
            carNumbersOfMotor.Insert(0, "Все");


            List<StolenCar> stolenCars = _db.StolenCars.Join(_db.Cars.Select(t => new Car
            {
                CarID = t.CarID,
                BrandID = t.BrandID,
                OwnerID = t.OwnerID,
                CarRegistrationNumber = t.CarRegistrationNumber,
                CarPhoto = t.CarPhoto,
                CarNumberOfBody = t.CarNumberOfBody,
                CarNumberOfMotor = t.CarNumberOfMotor,
                CarNumberOfPassport = t.CarNumberOfPassport,
                CarReleaseDate = t.CarReleaseDate,
                CarRegistrationDate = t.CarRegistrationDate,
                CarLastCheckupDate = t.CarLastCheckupDate,
                CarColor = t.CarColor,
                CarDescription = t.CarDescription,
                Brand = t.Brand,
                Owner = t.Owner
            }).ToList(), // второй набор
             p => p.CarID, // свойство-селектор объекта из первого набора
              c => c.CarID, // свойство-селектор объекта из второго набора
               (p, c) => new StolenCar
               {
                   StolenCarID = p.StolenCarID,
                   CarID = p.CarID,
                   EmployeeID = p.EmployeeID,
                   StolenCarStealingDate = p.StolenCarStealingDate,
                   StolenCarStatementDate = p.StolenCarStatementDate,
                   StolenCarInsuranceType = p.StolenCarInsuranceType,
                   StolenCarCondition = p.StolenCarCondition,
                   StolenCarFind = p.StolenCarFind,
                   StolenCarFindDate = p.StolenCarFindDate,
                   Car = c,
                   Employee = p.Employee
               }).OrderBy(s => s.StolenCarID).ToList();


            if (brandName != "Все" && brandName != null)
            {
                stolenCars = stolenCars.Where(c => c.Car.Brand.BrandName == brandName).ToList();
            }
            if (ownerName != "Все" && ownerName != null)
            {
                stolenCars = stolenCars.Where(c => c.Car.Owner.OwnerName == ownerName).ToList();
            }
            if (carNumberOfMotor != "Все" && carNumberOfMotor != null)
            {
                stolenCars = stolenCars.Where(c => c.Car.CarNumberOfMotor == carNumberOfMotor).ToList();
            }
            if (startRegistrationDate != null && endRegistrationDate != null && startRegistrationDate != "" && endRegistrationDate != "")
            {
                stolenCars = stolenCars.Where(c => (c.StolenCarStealingDate >= DateTimeConverter.getFromString(startRegistrationDate) &&
                     c.StolenCarStealingDate <= DateTimeConverter.getFromString(endRegistrationDate))).ToList();
            }
            if (isFind == "on" && isFind != null)
            {
                stolenCars = stolenCars.Where(c => c.StolenCarFind == "Не найдена").ToList();
            }

            StolenCarsFilter carsFilter = new StolenCarsFilter
            {
                brandName = brandName,
                ownerName = ownerName,
                BrandNames = brandNames,
                OwnerNames = ownerNames,
                carNumberOfMotor = carNumberOfMotor,
                CarNumbersOfMotor = carNumbersOfMotor,
                startRegistrationDate = startRegistrationDate,
                endRegistrationDate = endRegistrationDate,
                isFind = isFind
            };

            PageViewModel pageViewModel = new PageViewModel(stolenCars.Count, pageNumber, pageSize, carsFilter);

            StolenCarViewModel stolenCarViewModel = new StolenCarViewModel { PageViewModel = pageViewModel,
                StolenCars = stolenCars.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(),
                StolenCarsFilters = carsFilter
            };
            return View(stolenCarViewModel);
        }


        // GET: Cars/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stolenCar = getStolenCarById(id);
            if (stolenCar == null)
            {
                return NotFound();
            }
            ViewData["CarID"] = new SelectList(_db.Cars, "CarID", "CarRegistrationNumber", stolenCar.CarID);
            ViewData["EmployeeID"] = new SelectList(_db.Employees, "EmployeeID", "EmployeeName", stolenCar.EmployeeID);
            return View(stolenCar);
        }


        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("StolenCarID,CarID,EmployeeID," +
            "StolenCarStealingDate,StolenCarStatementDate,StolenCarInsuranceType,StolenCarCondition," +
            "StolenCarFind,StolenCarFindDate")] StolenCar stolenCar)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(stolenCar);
                    _db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StolenCarExists(stolenCar.StolenCarID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("StolenCars");
            }
            ViewData["CarID"] = new SelectList(_db.Cars, "CarID", "CarRegistrationNumber", stolenCar.CarID);
            ViewData["EmployeeID"] = new SelectList(_db.Employees, "EmployeeID", "EmployeeName", stolenCar.EmployeeID);
            return View(stolenCar);
        }


        // GET: Cars/Create
        public IActionResult Create()
        {
            ViewData["CarID"] = new SelectList(_db.Cars, "CarID", "CarRegistrationNumber");
            ViewData["EmployeeID"] = new SelectList(_db.Employees, "EmployeeID", "EmployeeName");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("StolenCarID,CarID,EmployeeID," +
            "StolenCarStealingDate,StolenCarStatementDate,StolenCarInsuranceType,StolenCarCondition," +
            "StolenCarFind,StolenCarFindDate")] StolenCar stolenCar)
        {
            if (ModelState.IsValid)
            {
                if (StolenCarExists(stolenCar.StolenCarID))
                {
                    return View("Message", "Уже существует автомобиль в угоне с данным идентификатором!");

                }
                _db.Add(stolenCar);
                _db.SaveChanges();
                return RedirectToAction("StolenCars");
            }

            ViewData["CarID"] = new SelectList(_db.Cars, "CarID", "CarRegistrationNumber", stolenCar.CarID);
            ViewData["EmployeeID"] = new SelectList(_db.Employees, "EmployeeID", "EmployeeName", stolenCar.EmployeeID);
            return View(stolenCar);
        }


        // GET: Cars/Details/5
        public IActionResult More(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stolenCar = getStolenCarById(id);

            if (stolenCar == null)
            {
                return NotFound();
            }

            return View(stolenCar);
        }



        // GET: Cars/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stolenCar = getStolenCarById(id);

            if (stolenCar == null)
            {
                return NotFound();
            }

            return View(stolenCar);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int StolenCarID)
        {
            var stolenCar = _db.StolenCars.Where(c => c.StolenCarID == StolenCarID);
            _db.StolenCars.RemoveRange(stolenCar);
            _db.SaveChanges();
            return RedirectToAction("StolenCars");
        }

        private bool StolenCarExists(int id)
        {
            return _db.StolenCars.Any(e => e.StolenCarID == id);
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private StolenCar getStolenCarById(int? id)
        {
            return _db.StolenCars.Select(t => new StolenCar
            {
                StolenCarID = t.StolenCarID,
                CarID = t.CarID,
                EmployeeID = t.EmployeeID,
                StolenCarStealingDate = t.StolenCarStealingDate,
                StolenCarStatementDate = t.StolenCarStatementDate,
                StolenCarInsuranceType = t.StolenCarInsuranceType,
                StolenCarCondition = t.StolenCarCondition,
                StolenCarFind = t.StolenCarFind,
                StolenCarFindDate = t.StolenCarFindDate,
                Car = t.Car,
                Employee = t.Employee
            }).Where(c => c.StolenCarID == id).ToList()[0];
        }
    }
}
