using CRM.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Entities.Leads
{
    public class Lead
    {
        [Key]
        public long LeadId { get; set; }

        public long CreatedById { get; set; }

        public long OwnerId { get; set; }

        [Display(Name = "عنوان")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public string Topic { get; set; }

        [Display(Name = "نام")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public string LastName { get; set; }

        [Display(Name = "شرکت")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public string Company { get; set; }

        public LeadStatus LeadStatus { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public bool IsDelete { get; set; } = false;

        public bool IsWin { get; set; } = false;

        #region Contact Info

        [Display(Name = "ایمیل")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
        public string? Email { get; set; }


        [Display(Name = "تلفن")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
        public string? Mobile { get; set; }


        [Display(Name = "توضیحات")]
        public string? Description { get; set; }

        #endregion

        #region Relations

        public User CreatedBy { get; set; }

        public User Owner { get; set; }

        #endregion

    }

    public enum LeadStatus
    {
        [Display(Name = "بسته شده")]
        Close,
        [Display(Name = "جدید")]
        New,
        [Display(Name = "فعال")]
        Active
    }
}
