using CRM.Domain.Entities.Actions;
using CRM.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.ViewModels.MarketingActions
{
    public class FilterMarketingActionViewModel : Paging<MarketingAction>
    {
        public long TaskId { get; set; }
    }
}
