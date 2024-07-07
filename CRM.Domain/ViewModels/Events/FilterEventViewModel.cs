using CRM.Domain.Entities.Events;
using CRM.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.ViewModels.Events
{
    public class FilterEventViewModel : Paging<Event>
    {
        public string? FilterTitle { get; set; }

        public string? StartFromDate { get; set; }

        public string? EndFromDate { get; set; }
    }
}
