using AutoMapper;
using ShiftsLogger.Model.DTOs;
using ShiftsLogger.Model.Entities;

namespace ShiftsLogger.Repository.Profiles;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
    }
}
