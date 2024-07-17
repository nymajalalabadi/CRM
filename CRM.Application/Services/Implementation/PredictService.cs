using CRM.Application.Services.Interface;
using CRM.DataLayer.Repository;
using CRM.Domain.Entities.Predict;
using CRM.Domain.Interfaces;
using CRM.Domain.ViewModels.Predict;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.Services.Implementation
{
    public class PredictService : IPredictService
    {
        #region Constructor

        private readonly IPredictRepository _predictRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;

        public PredictService(IPredictRepository predictRepository, IUserRepository userRepository, IOrderRepository orderRepository)
        {
            _predictRepository = predictRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
        }

        #endregion

        #region Methods

        public async Task<bool> ProcessMarketerPredict()
        {
            var marketers = await _userRepository.GetMarketerQueryable().Result.ToListAsync();

            var process = new List<PredictMarketerResult>();

            foreach (var marketer in marketers)
            {
                int zigmaDegree = 0;
                int allFinishedOrdersCount = 0;

                var orderMarketer = await _orderRepository.GetOrderSelectedMarketers().Result
                    .Where(s => s.MarketerId == marketer.UserId)
                    .Select(s => s.Order)
                    .Where(o => o.IsFinish && !o.IsDelete && o.EndDate != null)
                    .ToListAsync();

                foreach (var order in orderMarketer)
                {
                    var timeLeft = order.EndDate!.Value - order.CreateDate;

                    var predictDay = order.PredictDay;

                    zigmaDegree += (predictDay - timeLeft.Days);
                    allFinishedOrdersCount += 1;
                }

                if (allFinishedOrdersCount == 0)
                {
                    continue;
                }

                float deviation = zigmaDegree / allFinishedOrdersCount;

                process.Add(new PredictMarketerResult()
                {
                    MarketerId = marketer.UserId,
                    Deviation = deviation
                });
            }

            var resultDeviation = process.OrderBy(a => a.Deviation).FirstOrDefault();

            if (resultDeviation == null)
            {
                return false;
            }

            await DeleteAllMarketerPredict();

            var predictMarketer = new PredictMarketer()
            {
                MarketerId = resultDeviation.MarketerId
            };

            await _predictRepository.AddPredictMarketer(predictMarketer);
            await _predictRepository.SaveChanges();

            return true;
        }

        public async Task DeleteAllMarketerPredict()
        {
            var predicts = await _predictRepository.GetPredictMarketers().Result.ToListAsync();

            foreach (var predictMarketer in predicts)
            {
                _predictRepository.DeletePredictMarketer(predictMarketer);
            }

            await _predictRepository.SaveChanges();
        }

        #endregion
    }
}
