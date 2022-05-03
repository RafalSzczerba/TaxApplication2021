using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Tax1._0.WebApp.Models
{
    public class Salary
    {        
        public int Id { get; set; } 
        public string Email { get; set; }
        [Display(Name = "Miesiąc")]
        public int Month { get; set; }
        [Display(Name = "Obliczony podatek")]
        public int Tax { get; set; }
        [Display(Name = "Wynagrodzenie")]
        public decimal SalaryAmount { get; set; }            
        public bool Confimated { get; set; }
        public bool ModifyQuake { get; set; }
    }
}
