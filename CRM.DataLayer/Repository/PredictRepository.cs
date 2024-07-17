using CRM.DataLayer.Context;
using CRM.Domain.Entities.Predict;
using CRM.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataLayer.Repository
{
    public class PredictRepository: IPredictRepository
    {
        #region Constructor

        private CrmContext _context;

        public PredictRepository(CrmContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        public async Task AddPredictMarketer(PredictMarketer marketer)
        {
            await _context.PredictMarketer.AddAsync(marketer);
        }

        public async void UpdatePredictMarketer(PredictMarketer marketer)
        {
            _context.PredictMarketer.Update(marketer);
        }

        public async Task<PredictMarketer?> GetPredictMarketerById(long id)
        {
            return await _context.PredictMarketer.FirstOrDefaultAsync(p => p.Equals(id));
        }

        public async Task<IQueryable<PredictMarketer>> GetPredictMarketers()
        {
            return _context.PredictMarketer.AsQueryable();
        }

        public void DeletePredictMarketer(PredictMarketer marketer)
        {
            _context.PredictMarketer.Remove(marketer); 
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
