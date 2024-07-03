using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Domain.Entities.Orders;
using CRM.Domain.Entities.Companies;

namespace CRM.Domain.Entities.Account
{
	public class Customer
	{
		[Key, ForeignKey("User")]
		public long UserId { get; set; }

        public long? CompanyId { get; set; }

        [Display(Name = "شغل")]
		[MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
		public string? Job { get; set; }

		[Display(Name = "نام شرکت")]
		[MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
		public string? CompanyName { get; set; }

        public bool IsDelete { get; set; }

        #region Relations

        public User User { get; set; }

        public ICollection<Order> OrderCollection { get; set; }

        public Company Company { get; set; }

        #endregion
    }

}
