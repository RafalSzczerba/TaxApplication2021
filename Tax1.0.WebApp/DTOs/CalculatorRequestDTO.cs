using Calculator.Enums;
using System.ComponentModel.DataAnnotations;

namespace Tax1._0.WebApp.DTOs
{
    public class CalculatorRequestDTO
    {
        public string Emial { get; set; }
        public decimal currentSalary { get; set; }
        [Range(0.0, 1000000, ErrorMessage = "Wynagrodzenie nie może być ujemne")]

        [Display(Name = "Podaj bieżącą wypłatę w Euro")]
        public decimal currentSalaryEUR { get; set; }
        [Range(0.0, 1000000, ErrorMessage = "Wynagrodzenie nie może być ujemne")]

        [Display(Name = "Podaj bieżącą wypłatę w Dolarach")]
        public decimal currentSalaryUSD { get; set; }
        [Range(0.0, 1000000, ErrorMessage = "Wynagrodzenie nie może być ujemne")]

        [Display(Name = "Podaj bieżącą wypłatę w Funtach")]
        public decimal currentSalaryGBP { get; set; }
        [Range(0.0, 1000000, ErrorMessage = "Wynagrodzenie nie może być ujemne")]

        [Display(Name = "Podaj bieżącą wypłatę w PLN")]
        public decimal currentSalaryPLN { get; set; }
        [Display(Name = "Wybierz opcje kalkulacji")]
        //public int CalculationOption { get; set; }
        public SettlementType TaxType { get; set; }

    }
}
