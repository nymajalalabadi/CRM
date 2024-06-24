using CRM.Application.Services.Interface;
using CRM.Domain.Entities.Account;
using CRM.Domain.Interfaces;
using CRM.Domain.ViewModels.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.Services.Implementation
{
    public class UserService : IUserService
    {
        #region Constructor

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) 
        { 
            _userRepository = userRepository;   
        }

        #endregion

        #region Methods

        public async Task<FilterUserViewModel> FilterUser(FilterUserViewModel filter)
        {
            var query = await _userRepository.GetAllUsers();

            #region filter

            if (!string.IsNullOrEmpty(filter.FilterMobile))
            {
                query = query.Where(s => EF.Functions.Like(s.MobilePhone, $"%{filter.FilterMobile}%"));
            }

            if (!string.IsNullOrEmpty(filter.FilterLastName))
            {
                query = query.Where(s => EF.Functions.Like(s.LastName, $"%{filter.FilterLastName}%"));
            }

            if (!string.IsNullOrEmpty(filter.FilterFirstName))
            {
                query = query.Where(s => EF.Functions.Like(s.FirstName, $"%{filter.FilterFirstName}%"));
            }

            #endregion

            query = query.OrderByDescending(u => u.CreateDate);

            #region paging

            await filter.SetPaging(query);

            #endregion

            return filter;  
        }

        public async Task<AddMarketerResult> AddMarketer(AddMarketerViewModel marketer)
        {
            var user = new User()
            {
                FirstName = marketer.FirstName,
                Password = marketer.Password,
                LastName = marketer.LastName,
                UserName = marketer.UserName,
                Email = marketer.Email,
                MobilePhone = marketer.MobilePhone,
                IntroduceName = marketer.IntroduceName,
            };

            await _userRepository.AddUser(user);
            await _userRepository.SaveChangeAsync();

            var newMarketer = new Marketer()
            {
                UserId = user.UserId,
                FieldStudy = marketer.FieldStudy,
                Age = marketer.Age,
                IrCode = marketer.IrCode,
                Education = marketer.Education
            };

            await _userRepository.AddMarketer(newMarketer);
            await _userRepository.SaveChangeAsync();

            return AddMarketerResult.Success;
        }

        #endregion
    }
}
