using CRM.Application.Services.Interface;
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

        #region Actions

        public async Task<IActionResult> CreateOrder(long id)
        {
            ViewBag.customer = await _userService.GetCustomerById(id);

            if (ViewBag.customer == null)
            {
                return NotFound();
            }

            return View();
        }


        #endregion
    }
}
