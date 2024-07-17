using CRM.Domain.Entities.Orders;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.ViewModels.Orders
{
    public class CreateOrderViewModel
    {
        public long CustomerId { get; set; }

        [Display(Name = "عنوان سفارش")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
        [Required(ErrorMessage = "{0} اجباری است")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
        [Required(ErrorMessage = "{0} اجباری است")]
        public string Description { get; set; }

        [Display(Name = "تعداد روز پیشنهادی")]
        public int PredictDay { get; set; }

        [Display(Name = "نوع سفارش")]
        public OrderType OrderType { get; set; }

        public IFormFile? ImageFile { get; set; }
    }

    public enum CreateOrderResult
    {
        Success,
        Fail
    }
}
