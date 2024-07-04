﻿using CRM.Application.Services.Interface;
using CRM.Domain.ViewModels.Company;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers
{
    public class CompanyController : BaseController
    {
        #region Constructor

        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        #endregion

        #region List Company

        public async Task<IActionResult> FilterCompanies()
        {
            return View();
        }

        #endregion

        #region Create Company

        [HttpGet]
        public async Task<IActionResult> CreateCompany()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany(CreateCompanyViewModel create)
        {
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = "اطلاعات وارد شده معتبر نمی باشد";
                return View(create);
            }

            var result = await _companyService.CreateCompany(create);

            switch (result)
            {
                case CreateCompanyResult.Success:
                    TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                    return RedirectToAction("FilterCompanies");

                case CreateCompanyResult.Fail:
                    TempData[ErrorMessage] = " شماره موبایل کاربر موجود است ";
                    break;
            }

            return View(create);

        }

        #endregion
    }
}
