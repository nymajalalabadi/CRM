using CRM.Domain.ViewModels.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.Services.Interface
{
    public interface IEventService
    {
        #region Methods

        Task<AddEventResult> AddEvent(AddEventViewModel addEvent, long userId);

        Task<EditEventResult> EditEvent(EditEventViewModel editEvent);

        Task<EditEventViewModel> FillEditEventViewModel(long eventId);

        Task<FilterEventViewModel> FilterEvents(FilterEventViewModel filter);

        #endregion
    }
}
