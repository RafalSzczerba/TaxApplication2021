using Calculator;
using Calculator.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tax1._0.WebApp.DTOs;
using Tax1._0.WebApp.Models;

namespace Tax1._0.WebApp.Contracts
{
    public interface ITaxService
    {
        ApplicationUser FindUser(string email);
        Salary FindSalary(int id);
        void CreateNew(Salary salary);
        void UpdateTaxFreeAvaialabe(string email, decimal taxFree);
        void CreateNewRange(ApplicationUser user);
        List<Salary> GetAllSalary(string email);
        TaxCalculationOutput CalculateTax(ApplicationUser appUser, CalculatorRequestDTO request);
        decimal CalculateSalaryInDiffrentCurrency(List<SalaryCalculationInput> currentSalary);
        Task EditSalary(Salary salary);
        void ConfirmSalary(int id);
        void DeleteSalary(Salary salary);
        (decimal, int) GetAccumulatedSalaryAndTax(string email);
        void CalculateTaxCascade(ApplicationUser appUser, Salary salary);
    }
}