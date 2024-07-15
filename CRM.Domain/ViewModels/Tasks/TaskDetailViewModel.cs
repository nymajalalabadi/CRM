using CRM.Domain.Entities.Actions;
using CRM.Domain.Entities.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.ViewModels.Tasks
{
    public class TaskDetailViewModel
    {
        public long TaskId { get; set; }

        [Display(Name = "اولویت")]
        public int Priority { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "تاریخ انجام تسک")]
        public string UntilDate { get; set; }

        public string CreateDate { get; set; }

        [Display(Name = "وضعیت تکس")]
        public CrmTaskStatus CrmTaskStatus { get; set; }

        public Entities.Account.User User { get; set; }

        public int ActionCount { get; set; }

        public ICollection<MarketingAction> MarketingActions { get; set; }
    }
}
