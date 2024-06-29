using CRM.Application.Services.Interface;
using CRM.Domain.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers
{
	public class UserController : BaseController
    {
        #region Constructor

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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

        #endregion

        #region Create Marketer

        [HttpGet]
        public async Task<IActionResult> CreateMarketer(AddCustomerViewModel addCustomer)
        {
            if (!ModelState.IsValid)
            {
                return View(addCustomer);
            }

            var result = await _userService.AddCustomer(addCustomer);

            switch (result)
            {
                case AddCustomerResult.Success:
                    TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                    return RedirectToAction("Index");

                case AddCustomerResult.Fail:
                    TempData[ErrorMessage] = "عملیات با شکست مواجه شد";
                    break;
            }

            return View(addCustomer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMarketer(AddMarketerViewModel marketer)
        {
            if (!ModelState.IsValid)
            {
                return View(marketer);
            }

            var result = await _userService.AddMarketer(marketer);

            switch (result)
            {
                case AddMarketerResult.Success:
                    TempData[SuccessMessage] = "بازاریاب مورد نظر با موفقیت ثبت شد";
                    return RedirectToAction("Index");

                case AddMarketerResult.Fail:
                    TempData[ErrorMessage] = "مشکلی در ثبت اطلاعات میباشد";
                    ModelState.AddModelError("UserName", "مشکلی در ثبت اطلاعات میباشد");
                    break;
            }

            return View(marketer);
        }



		#endregion


		#endregion

		#region Edit User

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

		#endregion
	}
}
