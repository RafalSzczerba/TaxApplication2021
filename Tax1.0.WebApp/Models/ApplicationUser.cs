using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Tax1._0.WebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            CreatedAt = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Calculator.Enums.SettlementType TaxType { get; set; }
        public List<Salary> SalaryByMonth { get; set; }
        public decimal TaxFreeAvaialbe { get; set; }    
    }
}
