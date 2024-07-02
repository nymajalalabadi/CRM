using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.ViewModels.Orders
{
    public class OrderSelectMarketerViewModel
    {
        public long OrderId { get; set; }

        public long MarketerId { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
        public string Description { get; set; }
    }

    public enum AddOrderSelectMarketerResult
    {
        Success,
        Fail,
        SelectedMarketerExist
    }

}
