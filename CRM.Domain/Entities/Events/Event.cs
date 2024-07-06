using CRM.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Entities.Events
{
    public class Event
    {
        [Key]
        public long EventId { get; set; }

        public long UserId { get; set; }

        public EventType EventType { get; set; }

        [Display(Name = "عنوان")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public string Content { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public DateTime EventDate { get; set; }

        public bool IsDelete { get; set; } = false;

        #region Relations

        public User User { get; set; }

        #endregion
    }

    public enum EventType
    {
        [Display(Name = "هشدار")]
        Alert,
    }
}
