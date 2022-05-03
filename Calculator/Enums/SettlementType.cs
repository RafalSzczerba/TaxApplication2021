using System.ComponentModel.DataAnnotations;

namespace Calculator.Enums
{
    public enum SettlementType
    {
        [Display(Name = "Zwolniony")]
        Exempt,
        [Display(Name = "Liniowy")]
        Linear,
        [Display(Name = "Progresywny")]
        Progrssive
    }
}
