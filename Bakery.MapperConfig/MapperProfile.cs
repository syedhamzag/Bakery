using AutoMapper;
using Bakery.DataTransferObject.DTOs.Bakery;
using Bakery.DataTransferObject.DTOs.Role;
using Bakery.DataTransferObject.DTOs.Users;
using Bakery.Model.Models;

namespace Bakery.MapperConfig
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            #region Bakery
            CreateMap<PutBakery, Bakerys>();
            CreateMap<PostBakery, Bakerys>();
            CreateMap<Bakerys, GetBakery>();
            #endregion

            #region Role
            CreateMap<PutRole, Roles>()
                .ForMember(dto => dto.User, mem => mem.Ignore());
            CreateMap<PostRole, Roles>()
                .ForMember(dto => dto.User, mem => mem.Ignore());
            CreateMap<Roles, GetRole>();
            #endregion

            #region User
            CreateMap<PutUser, Users>()
                .ForMember(dto => dto.Role, mem => mem.Ignore());
            CreateMap<PostUser, Users>()
                .ForMember(dto => dto.Role, mem => mem.Ignore());
            CreateMap<Users, GetUser>()
                .ForMember(dto => dto.RoleName, mem => mem.Ignore());
            #endregion
        }
    }
}