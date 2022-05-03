using Calculator.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class CalcTax
    {      
        public static decimal CaluclateSalary(List<SalaryCalculationInput> currentSalary)
        {
            if (currentSalary == null)
            {
                return -1;
            }
            decimal calculatedSalary = 0m;
            decimal eurCalc = 4.6m;
            decimal usdCalc = 4.40m;
            decimal gbpCalc = 5.50m;

            foreach (var salary in currentSalary)
            {
                if (salary.Currency == Currency.EUR && salary.Ammount > 0)
                {
                    calculatedSalary = calculatedSalary + salary.Ammount * eurCalc;
                }
                else if (salary.Currency == Currency.USD && salary.Ammount > 0)
                {
                    calculatedSalary = calculatedSalary + salary.Ammount * usdCalc;
                }
                else if (salary.Currency == Currency.GBP && salary.Ammount > 0)
                {
                    calculatedSalary = calculatedSalary + salary.Ammount * gbpCalc;
                }
            }
            return calculatedSalary;
        }

        public static TaxCalculationOutput CalcualteCurrentTax(CalculatorInput input)
        {
            TaxCalculationOutput result = new TaxCalculationOutput();
            if (input.CurrentSalary<= 0)
            {
                result.TaxFreeAvaialableOutput = -1;
                return result;
            }
            if (input.TaxType == SettlementType.Progrssive)
            {
                result = CalcualteCurrentTaxAsProgressive(input);
            }
            else if (input.TaxType == SettlementType.Linear)
            {
                result = CalcualteCurrentTaxAsLinear(input);
            }
            return result;

        }

        private static TaxCalculationOutput CalcualteCurrentTaxAsProgressive(CalculatorInput input)
        {
            decimal secondTaxStep = 85528m;
            decimal taxAmountExculded = input.TaxFreeAvaialableInput;
            decimal totalSalary = 0m;
            TaxCalculationOutput result = new TaxCalculationOutput();
            if (input.Salary.Count > 0)
            {
                foreach (var item in input.Salary)
                {
                    totalSalary = totalSalary + item;
                }
            }
            if (totalSalary + input.CurrentSalary <= secondTaxStep) //I próg, ulga wliczona wcześniej
            {
                var calculatedTax = input.CurrentSalary * 0.17m - taxAmountExculded;
                if (calculatedTax < 0)
                {
                    result.TaxFreeAvaialableOutput = Math.Abs(calculatedTax);
                    result.TaxCalculated = 0;
                }
                else
                {
                    result.TaxCalculated = Decimal.ToInt32(calculatedTax);
                    result.TaxFreeAvaialableOutput = 0;
                } 
                
            }            
            if (totalSalary + input.CurrentSalary >= secondTaxStep) //II próg, ulga wliczona wcześniej
            {
                var calculatedTax = (totalSalary + input.CurrentSalary - secondTaxStep) * 0.32m + (secondTaxStep - totalSalary) * 0.17m - taxAmountExculded;
                if (calculatedTax < 0)
                {
                    result.TaxFreeAvaialableOutput = Math.Abs(calculatedTax);
                    result.TaxCalculated = 0;
                }
                else
                {
                    result.TaxCalculated = Decimal.ToInt32(calculatedTax);
                    result.TaxFreeAvaialableOutput = 0;
                }
            }
            if (totalSalary >= secondTaxStep) //II próg, ulga wliczona wcześniej
            {
                var calculatedTax = input.CurrentSalary * 0.32m;
                if (calculatedTax < 0)
                {
                    result.TaxFreeAvaialableOutput = Math.Abs(calculatedTax);
                    result.TaxCalculated = 0;
                }
                else
                {
                    result.TaxCalculated = Decimal.ToInt32(calculatedTax);
                    result.TaxFreeAvaialableOutput = 0;
                }
            }
            return result;
        }
        
        private static TaxCalculationOutput CalcualteCurrentTaxAsLinear(CalculatorInput input)
        {
            TaxCalculationOutput result = new TaxCalculationOutput();
            result.TaxCalculated = Decimal.ToInt32(input.CurrentSalary * 0.19m);
            result.TaxFreeAvaialableOutput = -1;
            return result;
        }
    }
}
