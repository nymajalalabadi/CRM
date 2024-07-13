using CRM.Domain.Entities.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.ViewModels.Tasks
{
    public class EditTaskViewModel
    {
        public long TaskId { get; set; }

        [Display(Name = "اولویت")]
        public int Priority { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "تاریخ انجام تسک")]
        public string UntilDate { get; set; }

        [Display(Name = "وضعیت تکس")]
        public CrmTaskStatus CrmTaskStatus { get; set; }
    }

    public enum EditTaskResult
    {
        Success,
        Fail
    }

}
