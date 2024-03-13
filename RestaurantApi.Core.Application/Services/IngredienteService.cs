using AutoMapper;
using Microsoft.AspNetCore.Http;
using RestaurantApi.Core.Application.Dtos.Account;
using RestaurantApi.Core.Application.Helpers;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Interfaces.Services;
using RestaurantApi.Core.Application.ViewModels.Ingrediente;
using RestaurantApi.Core.Application.ViewModels.Orden;
using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Core.Application.Services
{
    public class IngredienteService : GenericService<SaveIngredienteViewModel, IngredienteViewModel, Ingrediente>, IIngredienteService
    {
        private readonly IIngredienteRepository _ingredienteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse userViewModel;

        public IngredienteService(IIngredienteRepository ingredienteRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(ingredienteRepository, mapper)
        {
            _ingredienteRepository = ingredienteRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        } 

        public async Task<List<IngredienteViewModel>> GetAllIngredientsWithIncludeAsync()
        {
            var ingredientes = await _ingredienteRepository.GetAllAsync();
            var mappedIngredientes = _mapper.Map<List<IngredienteViewModel>>(ingredientes);
            return mappedIngredientes;
        }

    }
}
