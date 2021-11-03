using Microsoft.AspNetCore.Mvc;
using Q3_p1_Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Q3_p1_Contract.Controllers
{
    public class HomeController : Controller
    {
        PE_PRN_Sum21Context db = new PE_PRN_Sum21Context();

        public IActionResult Index()
        {
            var contracts = db.Contracts.ToList();
            var employees = db.Employees.ToList();
            var customers = db.Customers.ToList();
            foreach (var contract in contracts)
            {
                contract.Employee = (from e in employees
                                     where e.EmployeeId == contract.EmployeeId
                                     select e).FirstOrDefault();
                contract.Customer = (from c in customers
                                     where c.CustomerId == contract.CustomerId
                                     select c).FirstOrDefault();
            }
            ViewBag.contracts = contracts;
            ViewBag.customers = customers;
            return View();
        }

        [HttpPost]
        public IActionResult Index(int CustomerId)
        {
            var employees = db.Employees.ToList();
            var customers = db.Customers.ToList();
            if (CustomerId == 0)
            {
                var contracts = db.Contracts.ToList();
                foreach (var contract in contracts)
                {
                    contract.Employee = (from e in employees
                                         where e.EmployeeId == contract.EmployeeId
                                         select e).FirstOrDefault();
                    contract.Customer = (from c in customers
                                         where c.CustomerId == contract.CustomerId
                                         select c).FirstOrDefault();
                }

                ViewBag.contracts = contracts;
            }
            else
            {
                var contracts = db.Contracts.Where(c => c.CustomerId == CustomerId).ToList();
                foreach (var contract in contracts)
                {
                    contract.Employee = (from e in employees
                                         where e.EmployeeId == contract.EmployeeId
                                         select e).FirstOrDefault();
                    contract.Customer = (from c in customers
                                         where c.CustomerId == contract.CustomerId
                                         select c).FirstOrDefault();
                }

                ViewBag.contracts = contracts;
            }

            ViewBag.customers = db.Customers.ToList();
            ViewBag.id = CustomerId;
            return View();
        }
    }
}
