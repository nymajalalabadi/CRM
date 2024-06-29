using CRM.Application.Extensions;
using CRM.Application.Security;
using CRM.Application.Services.Interface;
using CRM.Application.StaticTools;
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

            if (!string.IsNullOrEmpty(filter.FilterEmail))
            {
                query = query.Where(s => EF.Functions.Like(s.Email, $"%{filter.FilterEmail}%"));
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
            if (marketer.ImageFile != null)
            {
                var imageProfileName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(marketer.ImageFile.FileName);

                marketer.ImageFile.AddImageToServer(imageProfileName, FilePath.UploadImageProfileServer, 280, 280);

                var userAvatar = new User()
                {
                    FirstName = marketer.FirstName,
                    Password = PasswordHelper.EncodePasswordMd5(marketer.Password.SanitizeText()),
                    LastName = marketer.LastName,
                    UserName = marketer.UserName,
                    Email = marketer.Email,
                    MobilePhone = marketer.MobilePhone,
                    IntroduceName = marketer.IntroduceName,
                    Gender = marketer.Gender,
                    ImageName = imageProfileName
                };

                await _userRepository.AddUser(userAvatar);
                await _userRepository.SaveChangeAsync();

                var marketerAvatar = new Marketer()
                {
                    UserId = userAvatar.UserId,
                    FieldStudy = marketer.FieldStudy,
                    Age = marketer.Age,
                    IrCode = marketer.IrCode,
                    Education = marketer.Education
                };

                await _userRepository.AddMarketer(marketerAvatar);
                await _userRepository.SaveChangeAsync();

                return AddMarketerResult.Success;
            }

            var user = new User()
            {
                FirstName = marketer.FirstName,
                Password = PasswordHelper.EncodePasswordMd5(marketer.Password),
                LastName = marketer.LastName,
                UserName = marketer.UserName,
                Email = marketer.Email,
                MobilePhone = marketer.MobilePhone,
                IntroduceName = marketer.IntroduceName,
                Gender = marketer.Gender,
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

        public async Task<EditMarketerViewModel> GetMarketerForEdit(long marketerId)
        {
            var user = await _userRepository.GetUserDetailById(marketerId);

            if (user == null)
            {
                return null;
            }

            var marketer = new EditMarketerViewModel()
            {
                UserId = user.UserId,
                Age = user.Marketer.Age,
                Education = user.Marketer.Education,
                Email = user.Email,
                FieldStudy = user.Marketer.FieldStudy,
                FirstName = user.FirstName,
                IntroduceName = user.IntroduceName,
                IrCode = user.Marketer.IrCode,
                LastName = user.LastName,
                MobilePhone = user.MobilePhone,
                UserName = user.UserName!,
                Gender = user.Gender,
                ImageName = user.ImageName
            };

            return marketer;
        }

        public async Task<EditMarketerResult> EditMarketer(EditMarketerViewModel marketer)
        {
            if (marketer.ImageFile != null)
            {
                var userAvatar = await _userRepository.GetUserById(marketer.UserId);

                if (userAvatar == null)
                {
                    return EditMarketerResult.Fail;
                }

                var imageProfileName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(marketer.ImageFile.FileName);
                marketer.ImageFile.AddImageToServer(imageProfileName, FilePath.UploadImageProfileServer, 280, 280, null, marketer.ImageName!);

                userAvatar.Email = marketer.Email;
                userAvatar.FirstName = marketer.FirstName;
                userAvatar.IntroduceName = marketer.IntroduceName;
                userAvatar.LastName = marketer.LastName;
                userAvatar.MobilePhone = marketer.MobilePhone;
                userAvatar.UserName = marketer.UserName;
                userAvatar.Gender = marketer.Gender;

                userAvatar.ImageName = imageProfileName;

                _userRepository.UpdateUser(userAvatar);

                var marketerAvatar = await _userRepository.GetMarketerById(marketer.UserId);

                if (marketerAvatar == null)
                {
                    return EditMarketerResult.Fail;
                }

                marketerAvatar!.Age = marketer.Age;
                marketerAvatar.Education = marketer.Education;
                marketerAvatar.FieldStudy = marketer.FieldStudy;
                marketerAvatar.IrCode = marketer.IrCode;

                _userRepository.UpdateMarketer(marketerAvatar);

                await _userRepository.SaveChangeAsync();

                return EditMarketerResult.Success;
            }

            var user = await _userRepository.GetUserById(marketer.UserId);

            if (user == null)
            {
                return EditMarketerResult.Fail;
            }

            user.Email = marketer.Email;
            user.FirstName = marketer.FirstName;
            user.IntroduceName = marketer.IntroduceName;
            user.LastName = marketer.LastName;
            user.MobilePhone = marketer.MobilePhone;
            user.UserName = marketer.UserName;
            user.Gender = marketer.Gender;

            _userRepository.UpdateUser(user);

            var Currentmarketer = await _userRepository.GetMarketerById(marketer.UserId);

            if (marketer == null)
            {
                return EditMarketerResult.Fail;
            }

            Currentmarketer!.Age = marketer.Age;
            Currentmarketer.Education = marketer.Education;
            Currentmarketer.FieldStudy = marketer.FieldStudy;
            Currentmarketer.IrCode = marketer.IrCode;

            _userRepository.UpdateMarketer(Currentmarketer);

            await _userRepository.SaveChangeAsync();

            return EditMarketerResult.Success;
        }

        public async Task<AddCustomerResult> AddCustomer(AddCustomerViewModel customer)
        {
            if (customer.ImageFile != null)
            {
                var imageProfileName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(customer.ImageFile.FileName);

                customer.ImageFile.AddImageToServer(imageProfileName, FilePath.UploadImageProfileServer, 280, 280);

                var userAvatar = new User()
                {
                    FirstName = customer.FirstName,
                    Password = PasswordHelper.EncodePasswordMd5(customer.Password.SanitizeText()),
                    LastName = customer.LastName,
                    UserName = customer.UserName,
                    Email = customer.Email,
                    MobilePhone = customer.MobilePhone,
                    IntroduceName = customer.IntroduceName,
                    Gender = customer.Gender,
                    ImageName = imageProfileName
                };

                await _userRepository.AddUser(userAvatar);
                await _userRepository.SaveChangeAsync();

                var customerAvatar = new Customer()
                {
                    UserId = userAvatar.UserId,
                    CompanyName = customer.CompanyName,
                    Job = customer.Job
                };

                await _userRepository.AddCustomer(customerAvatar);
                await _userRepository.SaveChangeAsync();

                return AddCustomerResult.Success;
            }

            var user = new User()
            {
                FirstName = customer.FirstName,
                Password = PasswordHelper.EncodePasswordMd5(customer.Password.SanitizeText()),
                LastName = customer.LastName,
                UserName = customer.UserName,
                Email = customer.Email,
                MobilePhone = customer.MobilePhone,
                IntroduceName = customer.IntroduceName,
                Gender = customer.Gender,
            };

            await _userRepository.AddUser(user);
            await _userRepository.SaveChangeAsync();

            var newCustomer = new Customer()
            {
                UserId = user.UserId,
                CompanyName = customer.CompanyName,
                Job = customer.Job
            };

            await _userRepository.AddCustomer(newCustomer);
            await _userRepository.SaveChangeAsync();

            return AddCustomerResult.Success;
        }

        public async Task<EditCustomerViewModel> GetCustomerForEdit(long customerId)
        {
            var user = await _userRepository.GetUserDetailById(customerId);

            if (user == null)
            {
                return null;
            }

            return new EditCustomerViewModel()
            {
                UserId = user.UserId,
                UserName = user.UserName!,
                Email = user.Email,
                MobilePhone = user.MobilePhone,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                IntroduceName = user.IntroduceName,
                ImageName = user.ImageName,
                Job = user.Customer.Job,
                CompanyName = user.Customer.CompanyName!
            };
        }

        public async Task<EditCustomerResult> EditCustomer(EditCustomerViewModel customer)
        {
            if (customer.ImageFile != null)
            {
                var userAvatar = await _userRepository.GetUserById(customer.UserId);

                if (userAvatar == null)
                {
                    return EditCustomerResult.Fail;
                }

                var imageProfileName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(customer.ImageFile.FileName);
                customer.ImageFile.AddImageToServer(imageProfileName, FilePath.UploadImageProfileServer, 280, 280, null, customer.ImageName!);

                userAvatar.Email = customer.Email;
                userAvatar.FirstName = customer.FirstName;
                userAvatar.IntroduceName = customer.IntroduceName;
                userAvatar.LastName = customer.LastName;
                userAvatar.MobilePhone = customer.MobilePhone;
                userAvatar.UserName = customer.UserName;
                userAvatar.Gender = customer.Gender;

                userAvatar.ImageName = imageProfileName;

                _userRepository.UpdateUser(userAvatar);

                var customerAvatar = await _userRepository.GetCustomerById(customer.UserId);

                if (customerAvatar == null)
                {
                    return EditCustomerResult.Fail;
                }

                customerAvatar!.CompanyName = customer.CompanyName;
                customerAvatar.Job = customer.Job;

                _userRepository.UpdateCustomer(customerAvatar);

                await _userRepository.SaveChangeAsync();

                return EditCustomerResult.Success;
            }

            var user = await _userRepository.GetUserById(customer.UserId);

            if (user == null)
            {
                return EditCustomerResult.Fail;
            }

            user.Email = customer.Email;
            user.FirstName = customer.FirstName;
            user.IntroduceName = customer.IntroduceName;
            user.LastName = customer.LastName;
            user.MobilePhone = customer.MobilePhone;
            user.UserName = customer.UserName;
            user.Gender = customer.Gender;


            _userRepository.UpdateUser(user);

            var currentCustomer = await _userRepository.GetCustomerById(customer.UserId);

            if (currentCustomer == null)
            {
                return EditCustomerResult.Fail;
            }

            currentCustomer!.CompanyName = customer.CompanyName;
            currentCustomer.Job = customer.Job;

            _userRepository.UpdateCustomer(currentCustomer);

            await _userRepository.SaveChangeAsync();

            return EditCustomerResult.Success;
        }

        public async Task<bool> DeleteUser(long userId)
        {
            var user = await _userRepository.GetUserDetailById(userId);

            if (user == null)
            {
                return false;
            }

            user.IsDelete = true;

            _userRepository.UpdateUser(user);

            if (user.Marketer != null)
            {
                var marketer = await _userRepository.GetMarketerById(userId);

                marketer!.IsDelete = true;

                 _userRepository.UpdateMarketer(marketer);
                await _userRepository.SaveChangeAsync();

                return true;
            }

            if (user.Customer != null)
            {
                var customer = await _userRepository.GetCustomerById(userId);

                customer!.IsDelete = true;

                _userRepository.UpdateCustomer(customer);
                await _userRepository.SaveChangeAsync();

                return true;
            }

            return false;
        }

        #endregion
    }
}
