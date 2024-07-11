using CRM.Domain.Entities.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Entities.Actions
{
    public class MarketingAction
    {
        [Key]
        public long ActionId { get; set; }

        public long CrmTaskId { get; set; }

        [Display(Name = "توضیحات")]
        public string? Description { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public bool IsDelete { get; set; } = false; 

        #region Relations

        public CrmTask CrmTask { get; set; }

        #endregion
    }

}
