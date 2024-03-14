using AutoMapper;
using RestaurantApi.Core.Application.Dtos.Account;
using RestaurantApi.Core.Application.ViewModels.Ingrediente;
using RestaurantApi.Core.Application.ViewModels.Mesa;
using RestaurantApi.Core.Application.ViewModels.Orden;
using RestaurantApi.Core.Application.ViewModels.Plato;
using RestaurantApi.Core.Application.ViewModels.User;
using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region UserProfile
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();
            #endregion

            #region Ingrediente
            CreateMap<Ingrediente, IngredienteViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ReverseMap();

            CreateMap<Ingrediente, SaveIngredienteViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ReverseMap();
            #endregion

            #region Mesa
                CreateMap<Mesa, MesaViewModel>()
                .ReverseMap();
                CreateMap<Mesa, SaveMesaViewModel>()
                .ReverseMap();
            #endregion

            #region Orden
            CreateMap<Orden, OrdenViewModel>()
                .ForMember(dest => dest.Platos, opt => opt.MapFrom(src => src.Platos))
                .ReverseMap();

            CreateMap<Orden, SaveOrdenViewModel>()
                .ForMember(dest => dest.PlatosIds, opt => opt.MapFrom(src => src.Platos.Select(p => p.Id)))
                .ReverseMap()
                .ForMember(dest => dest.Platos, opt => opt.Ignore())
                .ForMember(src => src.Estado, opt => opt.Ignore());

            CreateMap<Orden, UpdateOrdenViewModel>()
                .ForMember(dest => dest.PlatosIds, opt => opt.MapFrom(src => src.Platos.Select(p => p.Id)))
                .ReverseMap()
                .ForPath(src => src.Platos, opt => opt.Ignore()); 

            #endregion

            #region Plato
            CreateMap<Plato, PlatoViewModel>()
                .ForMember(dest => dest.Ingredientes, opt => opt.MapFrom(src => src.Ingredientes))
                .ReverseMap();

            CreateMap<Plato, SavePlatoViewModel>()
               .ForMember(dest => dest.IngredientesIds, opt => opt.MapFrom(src => src.Ingredientes.Select(i => i.Id)))
               .ReverseMap()
               .ForPath(dest => dest.Ingredientes, opt => opt.Ignore()); 

            #endregion
        }
    }
}
