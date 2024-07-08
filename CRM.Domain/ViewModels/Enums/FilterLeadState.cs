using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.ViewModels.Enums
{
    public enum FilterLeadState
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "درحال پیگیری")]
        Active,
        [Display(Name = "بسته شده")]
        Close,
        [Display(Name = "جدید")]
        New,
    }
}
