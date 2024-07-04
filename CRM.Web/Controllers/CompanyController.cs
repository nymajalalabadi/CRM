using CRM.Application.Services.Interface;
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

        public async Task<IActionResult> FilterCompanies(FilterCompanyViewModel filter)
        {
            var result = await _companyService.filterCompanyViewModel(filter);

            return View(result);
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

        #region Edit Company

        public async Task<IActionResult> EditCompany(long companyId)
        {
            var result = await _companyService.GetCompanyByEdit(companyId);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> EditCompany(EditCompanyViewModel companyViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = "اطلاعات وارد شده معتبر نمی باشد";
                return View(companyViewModel);
            }

            var result = await _companyService.EditCompany(companyViewModel);

            switch (result)
            {
                case EditCompanyResult.Success:
                    TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                    return RedirectToAction("FilterCompanies");

                case EditCompanyResult.Fail:
                    TempData[ErrorMessage] = "عملیات با شکست مواجه شد";
                    break;
            }

            return View(companyViewModel);
        }

        #endregion

        #region Delete Company

        public async Task<IActionResult> DeleteCompany(long companyId)
        {
            var result = await _companyService.DeleteCompany(companyId);

            if (result)
            {
                TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                return RedirectToAction("FilterCompanies");
            }

            TempData[ErrorMessage] = "عملیات با شکست مواجه شد";

            return RedirectToAction("FilterCompanies");
        }

        #endregion

    }
}
