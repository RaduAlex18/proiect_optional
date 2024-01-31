using AutoMapper;
using proiect_op_2_v3_final.Models;
using proiect_op_2_v3_final.Models.DTOs;

namespace proiect_op_2_v3_final.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            CreateMap<User, UserDTO>()
                .ForMember(ud => ud.FirstName,
                opts1 => opts1.MapFrom(u1 => u1.FirstName))
                .ForMember(ud => ud.LastName,
                opts2 => opts2.MapFrom(u2 => u2.LastName));
        }
    }
}
