using CRM.Domain.Entities.Events;
using CRM.Domain.Entities.Leads;
using CRM.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Entities.Account
{
	public class User
	{
		[Key]
		public long UserId { get; set; }

		[Display(Name = "نام کاربری")]
		[MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public string UserName { get; set; }

		[Display(Name = "کلمه عبور")]
		[MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public string Password { get; set; }

		[Display(Name = "نام")]
		[MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
		public string? FirstName { get; set; }

		[Display(Name = "نام خانوادگی")]
		[MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
		public string? LastName { get; set; }

		[Display(Name = "ایمیل")]
		[MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
		public string? Email { get; set; }

		[Display(Name = "نام تصویر")]
		[MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
		public string? ImageName { get; set; }

		[Display(Name = "شماره موبایل")]
		[MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public string MobilePhone { get; set; }

		[Display(Name = "نام معرف")]
		[MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
		public string? IntroduceName { get; set; }

		public Gender Gender { get; set; }

		public DateTime CreateDate { get; set; } = DateTime.Now;

		public bool IsDelete { get; set; }

		#region Relations

		public Marketer Marketer { get; set; }

		public Customer Customer { get; set; }

        public ICollection<OrderSelectedMarketer> OrderSelectedMarketers { get; set; }

        public ICollection<Event> Events { get; set; }

        public ICollection<Lead> CollectionLeadCreatedBy { get; set; }

        #endregion
    }

	public enum Gender
	{
		[Display(Name = "عمومی")]
		General,
		[Display(Name = "مرد")]
		Male,
		[Display(Name = "زن")]
		Female,
	}

}
