using CRM.Application.Services.Implementation;
using CRM.Application.Services.Interface;
using CRM.Domain.Entities.Account;
using CRM.Domain.ViewModels.Company;
using CRM.Domain.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers
{
    public class UserController : BaseController
    {
        #region Constructor

        private readonly IUserService _userService;

        private readonly ICompanyService _companyService;

        public UserController(IUserService userService, ICompanyService companyService)
        {
            _userService = userService;
            _companyService = companyService;
        }

        #endregion


        #region Actions

        #region User List

        [HttpGet]
        public async Task<IActionResult> Index(FilterUserViewModel filter)
        {
            var result = await _userService.FilterUser(filter);

            return View(result);
        }

        #endregion

        #region Add User

        #region Create Custome

        [HttpGet]
        public async Task<IActionResult> CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(AddCustomerViewModel addCustomer)
        {
            if (!ModelState.IsValid)
            {
                return View(addCustomer);
            }

            var result = await _userService.AddCustomer(addCustomer);

            switch (result)
            {
                case AddCustomerResult.Success:
                    TempData[SuccessMessage] = " مشتری مورد نظر با موفقیت ثبت شد";
                    return RedirectToAction("Index");

                case AddCustomerResult.Fail:
                    TempData[ErrorMessage] = "مشکلی در ثبت اطلاعات میباشد";
                    ModelState.AddModelError("UserName", "مشکلی در ثبت اطلاعات میباشد");
                    break;
            }

            return View(addCustomer);
        }

        #endregion

        #region Create Marketer

        [HttpGet]
        public async Task<IActionResult> CreateMarketer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMarketer(AddMarketerViewModel addMarketer)
        {
            if (!ModelState.IsValid)
            {
                return View(addMarketer);
            }

            var result = await _userService.AddMarketer(addMarketer);

            switch (result)
            {
                case AddMarketerResult.Success:
                    TempData[SuccessMessage] = "بازاریاب با موفقیت انجام شد";
                    return RedirectToAction("Index");

                case AddMarketerResult.Fail:
                    TempData[ErrorMessage] = "عملیات با شکست مواجه شد";
                    break;
            }

            return View(addMarketer);
        }

        #endregion


        #endregion

        #region Edit User

        #region Edit Customer

        [HttpGet]
        public async Task<IActionResult> EditCustomer(long id)
        {
            var result = await _userService.GetCustomerForEdit(id);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> EditCustomer(EditCustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = "اطلاعات وارد شده معتبر نمی باشد";
                return View(customerViewModel);
            }

            var result = await _userService.EditCustomer(customerViewModel);

            switch (result)
            {
                case EditCustomerResult.Success:
                    TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                    return RedirectToAction("Index");
                case EditCustomerResult.Fail:
                    TempData[ErrorMessage] = "عملیات با شکست مواجه شد";
                    break;
            }

            return View(customerViewModel);
        }


        #endregion

        #region Edit Marketer

        [HttpGet]
        public async Task<IActionResult> EditMarketer(long id)
        {
            var marketer = await _userService.GetMarketerForEdit(id);

            if (marketer == null)
            {
                return NotFound();
            }

            return View(marketer);
        }

        [HttpPost]
        public async Task<IActionResult> EditMarketer(EditMarketerViewModel editMarketer)
        {
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = "اطلاعات وارد شده کامل نمی باشد";
                return View(editMarketer);
            }

            var result = await _userService.EditMarketer(editMarketer);

            switch (result)
            {
                case EditMarketerResult.Success:
                    TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                    return RedirectToAction("Index");

                case EditMarketerResult.Fail:
                    TempData[ErrorMessage] = "عملیات با شکست مواجه شد";
                    break;
            }

            return View(editMarketer);
        }
        #endregion

        #endregion

        #region Delete User

        public async Task<IActionResult> DeleteUser(long userId)
        {
            var result = await _userService.DeleteUser(userId);

            if (result)
            {
                TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                return RedirectToAction("Index");
            }
            else
            {
                TempData[ErrorMessage] = "عملیات با شکست مواجه شد";
                return RedirectToAction("Index");
            }
        }

        #endregion

        #region Select Company To Customer

        [HttpGet]
        public async Task<IActionResult> SelectComponyModal(long userId)
        {
            var model = new CustomerSelectCompanyViewModel()
            {
                CustomerId = userId,
            };

            ViewBag.CompanyList = await _companyService.GetCompaniesList();

            return PartialView("_SelectComponyModal", model);
        }

        [HttpPost]
        public async Task<IActionResult> SelectComponyModal(CustomerSelectCompanyViewModel customerSelectCompany)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { status = "NotValid" });
            }

            var result = await _companyService.SelectCompanyForCustomer(customerSelectCompany);

            switch (result)
            {
                case AddCustomerSelectCompanyResult.Success:
                    return new JsonResult(new { status = "Success" });

                case AddCustomerSelectCompanyResult.Fail:
                    return new JsonResult(new { status = "Error" });

                case AddCustomerSelectCompanyResult.SelectedCustomerExist:
                    return new JsonResult(new { status = "Exist" });
            }

            return new JsonResult(new { status = "Error" });
        }

        #endregion

        #endregion
    }
}
