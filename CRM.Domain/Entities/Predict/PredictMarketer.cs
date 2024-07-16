using CRM.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Entities.Predict
{
    public class PredictMarketer
    {
        [Key]
        public long Id { get; set; }

        public long MarketerId { get; set; }

        #region Relations

        public Marketer Marketer { get; set; }

        #endregion
    }
}
