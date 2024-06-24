using CRM.Application.Services.Interface;
using CRM.Domain.Interfaces;
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



        #endregion
    }
}
