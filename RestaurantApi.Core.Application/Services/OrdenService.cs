using AutoMapper;
using Microsoft.AspNetCore.Http;
using RestaurantApi.Core.Application.Dtos.Account;
using RestaurantApi.Core.Application.Helpers;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Interfaces.Services;
using RestaurantApi.Core.Application.ViewModels.Orden;
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

        public async Task<List<OrdenViewModel>> GetAllWithIncludeAsync()
        {
            var ordenes = await _ordenRepository.GetAllWithIncludeAsync();
            var mappedOrdenes = _mapper.Map<List<OrdenViewModel>>(ordenes);
            return mappedOrdenes;
        }

        public async Task<OrdenViewModel> GetOrdenByIdWithIncludeAsync(int ordenId)
       {
            var orden = await _ordenRepository.GetOrdenById(ordenId); 
            var mappedOrden = _mapper.Map<OrdenViewModel>(orden);
            return mappedOrden;
       }

        public async Task<OrdenViewModel> GetOrdenByIdTableId(int tableId)
        {
            var orden = await _ordenRepository.GetOrdenByTableId(tableId);
            var mappedOrden = _mapper.Map<OrdenViewModel>(orden);
            return mappedOrden;
        }

        public async Task<SaveOrdenViewModel> AddOrdenWithPlatos(SaveOrdenViewModel saveOrdenVm)
        {
            var orden = _mapper.Map<Orden>(saveOrdenVm);
            orden = await _ordenRepository.AddOrdenWithPlatos(orden, saveOrdenVm.PlatosIds);
            var ordenViewModel = _mapper.Map<SaveOrdenViewModel>(orden);
            return ordenViewModel;
        }

        public async Task<OrdenViewModel> UpdateOrden(UpdateOrdenViewModel viewModel, int id)
        {
            var orden = _mapper.Map<Orden>(viewModel);
            var updatedOrden = await _ordenRepository.UpdateOrdenWithPlatos(id, orden, viewModel.PlatosIds);
            return _mapper.Map<OrdenViewModel>(updatedOrden);
        }

        public async Task<bool> DeleteOrdenWithPlatos(int orderId)
        {
            try
            {
                var orden = await _ordenRepository.GetOrdenById(orderId);

                if (orden == null)
                {
                    throw new KeyNotFoundException("No se encontró la orden con el ID especificado.");
                }

                await _ordenRepository.DeleteOrdenWithPlatos(orderId);
                return true;
            }
            catch
            {
                return false;
            }
        }









    }
}
