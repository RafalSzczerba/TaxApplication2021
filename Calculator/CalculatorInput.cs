using Calculator.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class CalculatorInput
    {
       public List<decimal> Salary { get; set; }
       public SettlementType TaxType { get; set; }
       public decimal CurrentSalary { get; set; }
       public decimal CurrentSalaryUSD { get; set; }
       public decimal CurrentSalaryGBP { get; set; }
       public decimal CurrentSalaryEUR { get; set; }
       public decimal CurrentSalaryPLN { get; set; }
       public decimal TaxFreeAvaialableInput { get; set; }


    }
}
