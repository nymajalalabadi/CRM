﻿using CRM.Application.Services.Interface;
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

        public async Task<IActionResult> Index(FilterUserViewModel filter)
        {
            filter.TakeEntity = 5;

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
        public async Task<IActionResult> CreateMarketer()
        {
            return View();
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

        #endregion
    }
}
