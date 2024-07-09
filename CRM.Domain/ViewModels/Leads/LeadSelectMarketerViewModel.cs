using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.ViewModels.Leads
{
    public class LeadSelectMarketerViewModel
    {
        public long LeadId { get; set; }

        public long MarketerId { get; set; }
    }

    public enum AddleadSelectMarketerResult
    {
        Success,
        Fail,
        SelectedMarketerExist
    }
}
