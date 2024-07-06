using CRM.DataLayer.Context;
using CRM.Domain.Entities.Events;
using CRM.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataLayer.Repository
{
    public class EventRepository : IEventRepository
    {
        #region Constructor

        private CrmContext _context;

        public EventRepository(CrmContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        public async Task<Event?> GetEventById(long eventId)
        {
            return await _context.Events.FirstOrDefaultAsync(e => e.EventId.Equals(eventId));
        }

        public async Task<IQueryable<Event>> GetEvents()
        {
            return _context.Events.Where(e => !e.IsDelete).AsQueryable();
        }

        public async Task AddEvent(Event myEvent)
        {
            await _context.Events.AddAsync(myEvent);
        }

        public void UpdateEvent(Event myEvent)
        {
            _context.Events.Update(myEvent);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
