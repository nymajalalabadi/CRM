using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.ViewModels.Predict
{
    public class PredictMarketerResult
    {
        public long MarketerId { get; set; }
        public float Deviation { get; set; }
    }

}
