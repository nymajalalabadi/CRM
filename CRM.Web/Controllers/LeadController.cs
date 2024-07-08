﻿using CRM.Application.Extensions;
using CRM.Application.Services.Interface;
using CRM.Domain.ViewModels.Leads;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers
{
    public class LeadController : BaseController
    {
        #region Constructor

        private readonly ILeadService _leadService;

        private readonly IUserService _userService;

        public LeadController(ILeadService leadService, IUserService userService)
        {
            _leadService = leadService;
            _userService = userService;
        }

        #endregion

        #region Actions

        #region Filter

        public async Task<IActionResult> FilterLeads(FilterLeadViewModel filter)
        {
            var model = await _leadService.FilterLeads(filter);

            ViewData["marketerList"] = await _userService.GetMarketerList();

            return View(model);
        }

        #endregion

        #region Create

        [HttpGet]
        public async Task<IActionResult> CreateLead()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateLead(CreateLeadViewModel createLead)
        {
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = "اطلاعات وارد شده معتبر نمی باشد";
                return View(createLead);
            }

            var result = await _leadService.CreateLead(createLead, User.GetUserId());

            switch (result)
            {
                case CreateLeadResult.Success:
                    TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                    return RedirectToAction("FilterLeads");

                case CreateLeadResult.Fail:
                    TempData[ErrorMessage] = "عملیات با شکست مواجه شد";
                    break;
            }

            return View(createLead);
        }

        #endregion

        #region Edit

        [HttpGet]
        public async Task<IActionResult> EditLead(long leadId)
        {
            var model = await _leadService.FillEditLeadViewModel(leadId);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditLead(EditLeadViewModel editLead)
        {
            if (!TryValidateModel(editLead))
            {
                TempData[WarningMessage] = "اطلاعات وارد شده معتبر نمیباشد";
                return View(editLead);
            }

            var result = await _leadService.EditLead(editLead);

            switch (result)
            {
                case EditLeadResult.Success:
                    TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                    return RedirectToAction("FilterLeads");

                case EditLeadResult.Fail:
                    TempData[ErrorMessage] = "عملیات با شکست مواجه شد";
                    break;
            }

            return View(editLead);
        }

        #endregion


        #endregion
    }
}
