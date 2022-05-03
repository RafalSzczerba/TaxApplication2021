using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tax1._0.WebApp.Data;
using Tax1._0.WebApp.Models;
using Tax1._0.WebApp.DTOs;
using System.Globalization;
using System;
using Calculator.Enums;
using Calculator;
using Tax1._0.WebApp.Contracts;

namespace Tax1._0.WebApp.Services
{
    public class TaxService: ITaxService
    {
        private readonly ApplicationDbContext _db;
        public TaxService(ApplicationDbContext db
           )
        {
            
            _db = db;
        }
        public ApplicationUser FindUser(string email)
        {
            var UsersQuery = _db.Users.AsQueryable().Where(q => q.Email == email).FirstOrDefault();
            return UsersQuery;
        }
        public Salary FindSalary(int id)
        {
            var UsersQuery = _db.Salary.AsQueryable().Where(q => q.Id == id).FirstOrDefault();
            return UsersQuery;
        }
        public void CreateNew(Salary salary)
        {
            try
            {
                _db.Salary.Add(salary);
                _db.SaveChanges();
            }
            catch
            {
            }            
        }
        public void UpdateTaxFreeAvaialabe(string email, decimal taxFree)
        {
            var UsersQuery = _db.Users.AsQueryable().Where(q => q.Email == email).FirstOrDefault();
            UsersQuery.TaxFreeAvaialbe = taxFree;
            _db.SaveChanges();

        }
        public void CreateNewRange(ApplicationUser user)
        {
            int monthOfRegistration;
            var usesr = _db.Users.Where(e => e.Email == user.Email).FirstOrDefault().CreatedAt;
            int.TryParse(_db.Users.Where(e => e.Email == user.Email).FirstOrDefault().CreatedAt.ToString("MM"), out monthOfRegistration);
            var monthToFullfill = new List<Salary>();
            for (int i = 1; i < monthOfRegistration; i++)
            {
                monthToFullfill.Add(new Salary
                {
                    Email = user.Email,
                    Month = i,
                    SalaryAmount = 0,
                    Confimated = false
                });
            }
            _db.Salary.AddRange(monthToFullfill);
            _db.SaveChanges();
        }
        public List<Salary> GetAllSalary(string email)
        {
            var result = _db.Salary.AsQueryable().Where(q => q.Email == email).ToList();
            if (result.Count > 0)
            {
                return result;
            }
            else
            {
                return new List<Salary>();
            }
        }
        public TaxCalculationOutput CalculateTax(ApplicationUser appUser, CalculatorRequestDTO request)
        {
            List<decimal> salaryList = new List<decimal>();
            var salaries = _db.Salary.AsQueryable().Where(e => e.Email == request.Emial).OrderBy(e=>e.Month).ToList();
            if(salaries.Last().Month == System.DateTime.UtcNow.Month)
            {
                _db.Salary.Remove(salaries.Last());
                salaries.Remove(salaries.Last());
            }
            foreach (var salary in salaries)
            {
                salaryList.Add(salary.SalaryAmount);
            }            
            
            var tax = Calculator.CalcTax.CalcualteCurrentTax(new Calculator.CalculatorInput
            {
                CurrentSalary = request.currentSalary,
                Salary = salaryList,
                TaxType = request.TaxType,
                TaxFreeAvaialableInput = appUser.TaxFreeAvaialbe
                
            });
            if(tax.TaxFreeAvaialableOutput != -1)
            {
                UpdateTaxFreeAvaialabe(appUser.Email, tax.TaxFreeAvaialableOutput);
            }
            return tax;
        }
        public decimal CalculateSalaryInDiffrentCurrency(List<SalaryCalculationInput> currentSalary)
        {
            var salary = Calculator.CalcTax.CaluclateSalary(currentSalary);
            return salary;
        }
        public async Task EditSalary(Salary salary)
        {
            var result = _db.Salary.AsQueryable().Where(q => q.Email == salary.Email && q.Id == salary.Id).SingleOrDefault();            
            if (result != null)
            {               
                result.SalaryAmount = salary.SalaryAmount;                
                await _db.SaveChangesAsync();
            }            
        }
        public void ConfirmSalary(int id)
        {
            var result = _db.Salary.AsQueryable().Where(q => q.Id == id).SingleOrDefault();
            if (result != null)
            {
                result.Confimated = true;
                _db.SaveChanges();
            }
        }
        public void DeleteSalary(Salary salary)
        {
            var result = _db.Salary.AsQueryable().Where(q => q.Email == salary.Email && q.Id == salary.Id).SingleOrDefault();
            if (result != null)
            {
                _db.Salary.Remove(result);
                _db.SaveChanges();
            }
        }
        public (decimal,int) GetAccumulatedSalaryAndTax(string email)
        {
            var result = _db.Salary.AsQueryable().Where(q => q.Email == email).ToList();
            decimal salaryTotal = 0m;
            int taxTotal = 0;
            foreach (var item in result)
            {
                salaryTotal = salaryTotal + item.SalaryAmount;
                taxTotal = taxTotal + item.Tax;
            }
            return(salaryTotal,taxTotal);   
        }
        public void CalculateTaxCascade(ApplicationUser appUser,Salary salary)
        {
            var salaries = _db.Salary.AsQueryable().Where(q => q.Email == salary.Email && q.Month > salary.Month).ToList();
            var user = _db.Users.Where(e=>e.Email == salary.Email).FirstOrDefault();
            DeleteSalary(salary);
            var tax = CalculateTax(appUser,new CalculatorRequestDTO
            {
                currentSalary = salary.SalaryAmount,
                Emial = salary.Email,
                TaxType = user.TaxType
           

            });
            CreateNew(new Salary
            {
                Email = salary.Email,
                Month = salary.Month,
                SalaryAmount = salary.SalaryAmount,
                Tax = tax.TaxCalculated,
                Confimated = true
            }) ;
            foreach (var item in salaries)
            {
                DeleteSalary(item);
                tax = CalculateTax(appUser,new CalculatorRequestDTO
                {
                    currentSalary = item.SalaryAmount,
                    Emial = item.Email,
                    TaxType = user.TaxType
                });
                CreateNew(new Salary
                {
                    SalaryAmount = item.SalaryAmount,
                    Month = item.Month,
                    Email = item.Email,
                    Tax = tax.TaxCalculated,
                });    
            }
        }
    }
}
