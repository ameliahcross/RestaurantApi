using AutoMapper;
using Microsoft.AspNetCore.Http;
using RestaurantApi.Core.Application.Dtos.Account;
using RestaurantApi.Core.Application.Helpers;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Interfaces.Services;
using RestaurantApi.Core.Application.ViewModels.Ingrediente;
using RestaurantApi.Core.Application.ViewModels.Orden;
using RestaurantApi.Core.Application.ViewModels.Plato;
using RestaurantApi.Core.Domain.Entities;
namespace RestaurantApi.Core.Application.Services
{
    public class OrdenService : GenericService<SaveOrdenViewModel, OrdenViewModel, Orden>, IOrdenService
    {
        private readonly IOrdenRepository _ordenRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse userViewModel;

        public OrdenService(IOrdenRepository ordenRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(ordenRepository, mapper)
        { 
            _ordenRepository = ordenRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }
       public async Task<List<OrdenViewModel>> GetAllOrdenesByIdWithIncludeAsync(int ordenId)
       {
            var ordenes = _ordenRepository.GetAllWithIncludeAsync(ordenId); 
            var mappedOrdenes = _mapper.Map<List<OrdenViewModel>>(ordenes);
            return mappedOrdenes;
       }
    }
}
