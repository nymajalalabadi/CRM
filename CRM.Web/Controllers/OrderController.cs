using CRM.Application.Services.Interface;
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
    }
}
