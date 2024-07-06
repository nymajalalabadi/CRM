using CRM.Application.Convertors;
using CRM.Application.Services.Interface;
using CRM.Domain.Entities.Events;
using CRM.Domain.Interfaces;
using CRM.Domain.ViewModels.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.Services.Implementation
{
    public class EventService : IEventService
    {
        #region Constructor

        private readonly IEventRepository _eventRepository;

        private readonly IUserRepository _userRepository;

        public EventService(IEventRepository eventRepository, IUserRepository userRepository)
        {
            _eventRepository = eventRepository;
            _userRepository = userRepository;
        }

        #endregion

        #region Methods

        public async Task<AddEventResult> AddEvent(AddEventViewModel addEvent, long userId)
        {
            if (string.IsNullOrEmpty(addEvent.Title) || string.IsNullOrEmpty(addEvent.Content))
            {
                return AddEventResult.Fail;
            }

            var newEvent = new Event()
            {
                Content = addEvent.Content,
                EventDate = addEvent.EventDate.ToMiladiDate(),
                EventType = addEvent.EventType,
                Title = addEvent.Title,
                UserId = userId
            };

            await _eventRepository.AddEvent(newEvent);
            await _eventRepository.SaveChanges();

            return AddEventResult.Success;
        }

        public async Task<EditEventResult> EditEvent(EditEventViewModel editEvent)
        {
            var currentEvent = await _eventRepository.GetEventById(editEvent.EventId);

            if (currentEvent == null)
            {
                return EditEventResult.Fail;
            }

            currentEvent.Content = editEvent.Content;
            currentEvent.EventDate = editEvent.EventDate.ToMiladiDate();
            currentEvent.EventType = editEvent.EventType;
            currentEvent.Title = editEvent.Title;

             _eventRepository.UpdateEvent(currentEvent);
            await _eventRepository.SaveChanges();

            return EditEventResult.Success;

        }

        public async Task<EditEventViewModel> FillEditEventViewModel(long eventId)
        {
            var currentEvent = await _eventRepository.GetEventById(eventId);

            if (currentEvent == null) 
            {
                return null;
            }

            return new EditEventViewModel()
            {
                EventId = currentEvent.EventId,
                EventType = currentEvent.EventType,
                Title = currentEvent.Title,
                Content = currentEvent.Content,
                EventDate = currentEvent.EventDate.ToShamsiDate(),
            };
        }

        public async Task<FilterEventViewModel> FilterEvents(FilterEventViewModel filter)
        {
            var query = await _eventRepository.GetEvents();

            query = query.OrderByDescending(c => c.CreateDate);

            #region paging

            await filter.SetPaging(query);

            #endregion

            return filter;
        }

        #endregion
    }
}
