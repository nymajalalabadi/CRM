using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.ViewModels.MarketingActions
{
    public class CreateMarketingActionViewModel
    {
        public long CrmTaskId { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }
    }

    public enum CreateMarketingActionResult
    {
        Success,
        Fail
    }
}
