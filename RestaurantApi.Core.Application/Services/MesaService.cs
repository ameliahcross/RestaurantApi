using AutoMapper;
using Microsoft.AspNetCore.Http;
using RestaurantApi.Core.Application.Dtos.Account;
using RestaurantApi.Core.Application.Helpers;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Interfaces.Services;
using RestaurantApi.Core.Application.ViewModels.Ingrediente;
using RestaurantApi.Core.Application.ViewModels.Mesa;
using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Core.Application.Services
{
    public class MesaService : GenericService<SaveMesaViewModel, MesaViewModel, Mesa>, IMesaService
    {
        private readonly IMesaRepository _mesaRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse userViewModel;

        public MesaService(IMesaRepository mesaRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mesaRepository, mapper)
        {
            _mesaRepository = mesaRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task<MesaViewModel> ChangeMesaStatusAsync(int mesaId, EstadoMesa newStatus)
        {
            var mesa = await _mesaRepository.ChangeMesaStatus(mesaId, newStatus);
            return _mapper.Map<MesaViewModel>(mesa);
        }
    }
}
