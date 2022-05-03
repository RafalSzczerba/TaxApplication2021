using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tax1._0.WebApp.Models;
using Tax1._0.WebApp.Services;
using Tax1._0.WebApp.DTOs;
using System.Threading.Tasks;
using System;
using Calculator.Enums;
using System.Collections.Generic;
using Calculator;
using System.Linq;
using Tax1._0.WebApp.Contracts;

namespace Tax1._0.WebApp.Controllers
{
    public class TaxController : Controller
    {


        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITaxService _taxService;

        public TaxController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, ITaxService taxService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _taxService = taxService;


        }
        // GET: TaxController
        public ActionResult Index()
        {
            var userLogon = HttpContext.User.Identity.Name;
            var currentUser = _taxService.FindUser(userLogon);
            var model = _taxService.GetAllSalary(currentUser.Email);
            List<Salary> salaries = new List<Salary>();      
            foreach (var item in model)
            {
                if (!item.Confimated)
                {
                    salaries.Add(item);
                }
            }
            var temp = salaries.OrderBy(m=>m.Month).FirstOrDefault();
            Salary recordToFill = new Salary();
            if(salaries.Count() > 0)
            {
                recordToFill = model.Where(m => m.Month == temp.Month).FirstOrDefault();

            }
            salaries.Clear();
            foreach (var item in model)
            {
                if(item.Month == recordToFill.Month)
                {
                    item.ModifyQuake = true;
                }
                salaries.Add(item);
            }
            var stats = _taxService.GetAccumulatedSalaryAndTax(currentUser.Email);
            ViewBag.TotalSalary = stats.Item1;
            ViewBag.TotalTax = stats.Item2;
            return View(salaries);
        }
        public ActionResult SalaryConfirmation(int id)
        {
            _taxService.ConfirmSalary(id);
            return RedirectToAction(nameof(Index));
        }
        public ActionResult CalculateTax(CalculatorRequestDTO calculatorRequest)
        {
            var userLogon = HttpContext.User.Identity.Name;
            var currentUser = _taxService.FindUser(userLogon);
            TaxCalculationOutput calculatedTax = new TaxCalculationOutput();
            List<SalaryCalculationInput> salaryByCurrency = new List<SalaryCalculationInput>();
            var calculatedTaxByDiffentCurrency = _taxService.CalculateSalaryInDiffrentCurrency(
                new List<SalaryCalculationInput>
                {
                 new SalaryCalculationInput
                 {
                     Ammount = calculatorRequest.currentSalaryGBP,
                     Currency = Currency.GBP
                 },
                 new SalaryCalculationInput
                 {
                     Ammount = calculatorRequest.currentSalaryEUR,
                     Currency = Currency.EUR
                 },
                  new SalaryCalculationInput
                 {
                     Ammount = calculatorRequest.currentSalaryUSD,
                     Currency = Currency.USD
                 },
                });
            var totalSalary = calculatorRequest.currentSalaryPLN + calculatedTaxByDiffentCurrency;
            if (currentUser != null)
            {
                calculatorRequest.Emial = currentUser.Email;
                calculatorRequest.TaxType = currentUser.TaxType;
                calculatorRequest.currentSalary = totalSalary;
                calculatedTax = _taxService.CalculateTax(currentUser,calculatorRequest);
                _taxService.CreateNew(new Salary
                {
                    Email = currentUser.Email,
                    Month = DateTime.UtcNow.Month,
                    SalaryAmount = calculatorRequest.currentSalary,
                    Tax = calculatedTax.TaxCalculated,
                });
                _taxService.UpdateTaxFreeAvaialabe(currentUser.Email, calculatedTax.TaxFreeAvaialableOutput);
            }
            if (calculatorRequest != null)
            {
                ViewBag.Tax = calculatedTax.TaxCalculated;
            }
            return View();
        }
        // GET: TaxController/Details/5
        public ActionResult Details(int id)
        {           
            return View();
        }

        // GET: TaxController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaxController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TaxController/Edit/5
        public ActionResult Edit(int id)
        {
            var userLogon = HttpContext.User.Identity.Name;
            var currentUser = _taxService.FindUser(userLogon);
            var model = _taxService.FindSalary(id);
            return View(model);
        }

        // POST: TaxController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Salary salary)
        {
            try
            {
                
                var userLogon = HttpContext.User.Identity.Name;
                var currentUser = _taxService.FindUser(userLogon);
                salary.Email = currentUser.Email;
                _taxService.CalculateTaxCascade(currentUser, salary);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TaxController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TaxController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Salary salary)
        {
            try
            {
                var userLogon = HttpContext.User.Identity.Name;
                var currentUser = _taxService.FindUser(userLogon);
                salary.Email = currentUser.Email;
                _taxService.DeleteSalary(salary);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
