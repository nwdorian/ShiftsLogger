using AutoMapper;
using ShiftsLogger.Model;
using ShiftsLogger.WebApi.RestModels;

namespace ShiftsLogger.WebApi.Profiles;

public class ShiftProfile : Profile
{
    public ShiftProfile()
    {
        CreateMap<Shift, ShiftRead>();
        CreateMap<ShiftCreate, Shift>();
    }
}
