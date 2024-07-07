using CRM.Domain.Entities.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.ViewModels.Events
{
    public class AddEventViewModel
    {
        public EventType EventType { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string EventDate { get; set; }
    }

    public enum AddEventResult
    {
        Success,
        Fail
    }
}
