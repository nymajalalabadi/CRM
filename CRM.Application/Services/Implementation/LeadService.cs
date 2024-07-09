using CRM.Application.Services.Interface;
using CRM.Domain.Entities.Leads;
using CRM.Domain.Interfaces;
using CRM.Domain.ViewModels.Enums;
using CRM.Domain.ViewModels.Leads;
using Microsoft.EntityFrameworkCore;

namespace CRM.Application.Services.Implementation
{
    public class LeadService : ILeadService
    {
        #region Constructor

        private readonly ILeadRepository _leadRepository;
        private readonly IUserRepository _userRepository;

        public LeadService(ILeadRepository leadRepository, IUserRepository userRepository)
        {
            _leadRepository = leadRepository;
            _userRepository = userRepository;
        }

        #endregion

        #region Methods

        public async Task<FilterLeadViewModel> FilterLeads(FilterLeadViewModel filter)
        {
            var query = await _leadRepository.GetLeads();

            #region MyRegion

            if (!string.IsNullOrEmpty(filter.FilterLeadTopic))
            {
                query = query.Where(l => l.Topic!.Equals(filter.FilterLeadTopic));
            }

            if (!string.IsNullOrEmpty(filter.FilterLeadName))
            {
                query = query.Where(a => EF.Functions.Like(a.FirstName + " " + a.LastName, $"%{filter.FilterLeadName}%"));
            }

            #endregion

            #region switch

            switch (filter.FilterLeadState)
            {
                case FilterLeadState.All:
                    break;

                case FilterLeadState.Active:
                    query = query.Where(a => a.LeadStatus == LeadStatus.Active);
                    break;

                case FilterLeadState.Close:
                    query = query.Where(a => a.LeadStatus == LeadStatus.Close);
                    break;

                case FilterLeadState.New:
                    query = query.Where(a => a.LeadStatus == LeadStatus.New);
                    break;
            }

            #endregion

            query = query.OrderByDescending(l => l.CreateDate);

            #region paging

            await filter.SetPaging(query);

            #endregion

            return filter;
        }

        public async Task<CreateLeadResult> CreateLead(CreateLeadViewModel createLead, long userId)
        {
            var user = await _userRepository.GetUserById(userId);

            if (user == null)
            {
                return CreateLeadResult.Fail;
            }

            if (string.IsNullOrEmpty(createLead.Topic))
            {
                return CreateLeadResult.Fail;
            }

            var lead = new Lead()
            {
                Company = createLead.Company,
                CreatedById = user.UserId,
                OwnerId = user.UserId,
                Description = createLead.Description!,
                LastName = createLead.LastName,
                Topic = createLead.Topic,
                Mobile = createLead.Mobile!,
                FirstName = createLead.FirstName,
                LeadStatus = LeadStatus.New,
            };

            await _leadRepository.AddLead(lead);
            await _leadRepository.SaveChanges();

            return CreateLeadResult.Success;
        }

        public async Task<EditLeadViewModel> FillEditLeadViewModel(long leadId)
        {
           var lead = await _leadRepository.GetLeadById(leadId);

            if (lead == null)
            {
                return null;
            }

            var result = new EditLeadViewModel()
            {
                LeadId = lead.LeadId,
                Company = lead.Company,
                Description = lead.Description,
                Email = lead.Email,
                LastName = lead.LastName,
                Topic = lead.Topic,
                Mobile = lead.Mobile,
                FirstName = lead.FirstName,
                IsWin= lead.IsWin,
                LeadStatus = lead.LeadStatus
            };

            return result;
        }

        public async Task<EditLeadResult> EditLead(EditLeadViewModel editLead)
        {
            var lead = await _leadRepository.GetLeadById(editLead.LeadId);

            if (lead == null)
            {
                return EditLeadResult.Fail;
            }

            lead.Company = editLead.Company;
            lead.Description = editLead.Description!;
            lead.Email = editLead.Email!;
            lead.LastName = editLead.LastName;
            lead.Topic = editLead.Topic;
            lead.Mobile = editLead.Mobile!;
            lead.FirstName = editLead.FirstName;
            lead.IsWin = editLead.IsWin;
            lead.LeadStatus = editLead.LeadStatus;  

            _leadRepository.UpdateLead(lead);
            await _leadRepository.SaveChanges();

            return EditLeadResult.Success;
        }

        public async Task<bool> DeleteLead(long leadId)
        {
            var lead = await _leadRepository.GetLeadById(leadId);

            if (lead == null)
            {
                return false;
            }

            lead.IsDelete = true;

            _leadRepository.UpdateLead(lead);
            await _leadRepository.SaveChanges();

            return true;
        }

        public async Task<AddleadSelectMarketerResult> SetLeadToMarketer(LeadSelectMarketerViewModel leadSelectMarketer)
        {
            var lead = await _leadRepository.GetLeadById(leadSelectMarketer.LeadId);
            var marketer = await _userRepository.GetMarketerById(leadSelectMarketer.MarketerId);

            if (lead == null || marketer == null)
            {
                return AddleadSelectMarketerResult.Fail;
            }

            if (lead.OwnerId == marketer.UserId)
            {
                return AddleadSelectMarketerResult.SelectedMarketerExist;
            }

            lead.OwnerId = marketer.UserId;

            _leadRepository.UpdateLead(lead);
            await _leadRepository.SaveChanges();

            return AddleadSelectMarketerResult.Success;
        }

        public async Task<bool> SetLeadToMarketer(long leadId, long marketerId)
        {
            var lead = await _leadRepository.GetLeadById(leadId);
            var marketer = await _userRepository.GetMarketerById(marketerId);

            if (lead == null || marketer == null)
            {
                return false;
            }

            lead.OwnerId = marketerId;

            _leadRepository.UpdateLead(lead);
            await _leadRepository.SaveChanges();

            return true;
        }


        #endregion
    }
}
