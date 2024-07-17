using CRM.Domain.Entities.Predict;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Interfaces
{
    public interface IPredictRepository
    {
        #region Methods

        Task AddPredictMarketer(PredictMarketer marketer);

        void UpdatePredictMarketer(PredictMarketer marketer);

        Task<PredictMarketer?> GetPredictMarketerById(long id);

        Task<IQueryable<PredictMarketer>> GetPredictMarketers();

        void DeletePredictMarketer(PredictMarketer marketer);

        Task SaveChanges();

        #endregion
    }

}
