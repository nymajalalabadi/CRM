using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.ViewModels.Tasks
{
    public class CreateTaskViewModel
    {
        public long MarketerId { get; set; }

        public long? OrderId { get; set; }

        [Display(Name = "اولویت")]
        public int Priority { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "تاریخ انجام تسک")]
        public string UntilDate { get; set; }
    }

    public enum CreateTaskResult
    {
        Success,
        Fail
    }
}
