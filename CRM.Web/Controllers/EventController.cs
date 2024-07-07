using CRM.Application.Extensions;
using CRM.Application.Services.Interface;
using CRM.Domain.ViewModels.Events;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers
{
    public class EventController : BaseController
    {
        #region Constructor

        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        #endregion

        #region Actions

        #region Events List

        public async Task<IActionResult> FilterEvents(FilterEventViewModel filter)
        {
            var result = await _eventService.FilterEvents(filter);

            return View(result);
        }

        #endregion

        #region Create Event

        [HttpGet]
        public async Task<IActionResult> CreateEvent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(AddEventViewModel addEvent)
        {
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = "اطلاعات وارد شده معتبر نمیباشد";
                return View(addEvent);
            }

            var result = await _eventService.AddEvent(addEvent, User.GetUserId());

            switch (result)
            {
                case AddEventResult.Success:
                    TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                    return RedirectToAction("FilterEvents");

                case AddEventResult.Fail:
                    TempData[ErrorMessage] = "عملیات با شکست مواجه شد";
                    break;
            }

            return View(addEvent);
        }

        #endregion

        #region Edit Event

        public async Task<IActionResult> EditEvent(long eventId)
        {
            var model = await _eventService.FillEditEventViewModel(eventId);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditEvent(EditEventViewModel editEvent)
        {
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = "اطلاعات وارد شده معتبر نمی باشد";
                return View(editEvent);
            }

            var result = await _eventService.EditEvent(editEvent);

            switch (result)
            {
                case EditEventResult.Success:
                    TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                    return RedirectToAction("FilterEvents");

                case EditEventResult.Fail:
                    TempData[ErrorMessage] = "عملیات با شکست مواجه شد";
                    break;
            }

            return View(editEvent);
        }

        #endregion

        #region Delete

        public async Task<IActionResult> DeleteEvent(long eventId)
        {
            var result = await _eventService.DeleteEvent(eventId);

            if (result)
            {
                TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
            }
            else
            {
                TempData[ErrorMessage] = "عملیات با شکست مواجه شد";
            }

            return RedirectToAction("FilterEvents");
        }

        #endregion


        #endregion
    }
}
