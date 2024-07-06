using CRM.Domain.Entities.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Interfaces
{
    public interface IEventRepository
    {
        #region Methods

        Task<Event?> GetEventById(long eventId);

        Task<IQueryable<Event>> GetEvents();

        Task AddEvent(Event myEvent);

        void UpdateEvent(Event myEvent);

        Task SaveChanges();


        #endregion
    }
}
