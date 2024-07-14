﻿using CRM.Domain.Entities.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.ViewModels.Tasks
{
    public class TaskDetailViewModel
    {
        [Display(Name = "اولویت")]
        public int Priority { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "تاریخ انجام تسک")]
        public string UntilDate { get; set; }

        public string CreateDate { get; set; }

        [Display(Name = "وضعیت تکس")]
        public CrmTaskStatus CrmTaskStatus { get; set; }

        public Entities.Account.User User { get; set; }
    }
}