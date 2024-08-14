using AutoMapper;
using ShiftsLogger.DAL;
using ShiftsLogger.Model;

namespace ShiftsLogger.Repository.Profiles;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity, User>();
    }
}
