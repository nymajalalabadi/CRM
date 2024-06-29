using CRM.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Entities.Orders
{
    public class OrderSelectedMarketer
    {
        [Key, ForeignKey("Order")]
        public long OrderId { get; set; }

        public long MarketerId { get; set; }

        public long ModifyUserId { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
        public string? Description { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public bool IsDelete { get; set; } = false;

        #region Relations

        public Order Order { get; set; }

        public Marketer Marketer { get; set; }

        public User ModifyUser { get; set; }

        #endregion
    }

}
