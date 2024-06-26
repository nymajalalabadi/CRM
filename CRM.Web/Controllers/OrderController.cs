﻿using CRM.Application.Services.Interface;
using CRM.Domain.ViewModels.Orders;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers
{
    public class OrderController : BaseController
    {
        #region Constructor

        private readonly IOrderService _orderService;

        private readonly IUserService _userService;

        public OrderController(IOrderService orderService, IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }

        #endregion

        #region Filter Orders

        public async Task<IActionResult> FilterOrders(FilterOrderViewModel filter)
        {
            var result = await _orderService.FilterOrder(filter);

            return View(result);
        }

        #endregion

        #region Create Order

        [HttpGet]
        public async Task<IActionResult> CreateOrder(long id)
        {
            ViewBag.customer = await _userService.GetCustomerById(id);

            if (ViewBag.customer == null)
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderViewModel orderViewModel)
        {
            ViewBag.customer = await _userService.GetCustomerById(orderViewModel.CustomerId);

            if (!ModelState.IsValid)
            {
                ViewBag.customer = await _userService.GetCustomerById(orderViewModel.CustomerId);

                TempData[WarningMessage] = "اطلاعات وارد شده صحیح نمی باشد";
                return View(orderViewModel);
            }

            var result = await _orderService.CreateOrder(orderViewModel);

            switch (result)
            {
                case CreateOrderResult.Success:
                    TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                    return RedirectToAction("FilterOrders");
                case CreateOrderResult.Fail:
                    TempData[ErrorMessage] = "عملیات با شکست مواجه شد";
                    break;
            }

            return View(orderViewModel);
        }

        #endregion

        #region Edit order

        public async Task<IActionResult> EditOrder(long orderId)
        {
            var result = await _orderService.FillEditOrderViewModel(orderId);

            ViewBag.customer = await _userService.GetCustomerById(result.CustomerId);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> EditOrder(EditOrderViewModel editOrder)
        {
            ViewBag.customer = await _userService.GetCustomerById(editOrder.CustomerId);

            if (!ModelState.IsValid)
            {
                ViewBag.customer = await _userService.GetCustomerById(editOrder.CustomerId);
                TempData[WarningMessage] = "اطلاعات وارد شده معتبر نمی باشد";
                return View(editOrder);
            }

            var result = await _orderService.EditOrder(editOrder);

            switch (result)
            {
                case EditOrderResult.Success:
                    TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                    return RedirectToAction("FilterOrders");

                case EditOrderResult.Fail:
                    TempData[ErrorMessage] = "عملیات با شکست مواجه شد";
                    break;
            }

            return View(editOrder);
        }

        #endregion

        #region Delete Order

        public async Task<IActionResult> DeleteOrder(long orderId)
        {
            var result = await _orderService.DeleteOrder(orderId);

            if (result)
            {
                TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                return RedirectToAction("FilterOrders");
            }

            TempData[ErrorMessage] = "عملیات با شکست مواجه شد";
            return RedirectToAction("FilterOrders");
        }

        #endregion


    }
}
