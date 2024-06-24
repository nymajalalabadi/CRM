using CRM.Domain.Entities.Account;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.ViewModels.User
{
    public class AddMarketerViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "این فیلد اجباری است")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
        public string UserName { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "این فیلد اجباری است")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
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

        [Display(Name = "شماره موبایل")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
        public string? MobilePhone { get; set; }

        [Display(Name = "نام معرف")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
        public string? IntroduceName { get; set; }

        [Display(Name = "رشته تحصیلی")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
        public string? FieldStudy { get; set; }

        [Display(Name = "سن")]
        public int? Age { get; set; }

        [Display(Name = "کد ملی")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
        public string? IrCode { get; set; }

        public Education Education { get; set; }

        public IFormFile? ImageFile { get; set; }
    }

    public enum AddMarketerResult
    {
        Success,
        Fail
    }

}
