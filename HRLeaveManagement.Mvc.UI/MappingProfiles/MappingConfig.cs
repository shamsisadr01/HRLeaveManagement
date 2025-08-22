using AutoMapper;
using HRLeaveManagement.Mvc.UI.Models.LeaveType;
using HRLeaveManagement.Mvc.UI.Services.Base;

namespace HRLeaveManagement.Mvc.UI.MappingProfiles;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<LeaveTypeDto, LeaveTypeVM>().ReverseMap();
        CreateMap<CreateLeaveTypeCommand, LeaveTypeVM>().ReverseMap();
        CreateMap<UpdateLeaveTypeCommand, LeaveTypeVM>().ReverseMap();
    }
}