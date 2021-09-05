using AutoMapper;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Domain.Common;
using zbw.Auftragsverwaltung.Domain.Users;

namespace zbw.Auftragsverwaltung.Core.Users.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<PaginatedList<User>, PaginatedList<UserDto>>();
        }
    }
}
