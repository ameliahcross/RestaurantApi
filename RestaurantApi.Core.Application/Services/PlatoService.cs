using AutoMapper;
using Microsoft.AspNetCore.Http;
using RestaurantApi.Core.Application.Dtos.Account;
using RestaurantApi.Core.Application.Helpers;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Interfaces.Services;
using RestaurantApi.Core.Application.ViewModels.Plato;
using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Core.Application.Services
{
    public class PlatoService : GenericService<SavePlatoViewModel, PlatoViewModel, Plato>, IPlatoService
    {
        private readonly IPlatoRepository _platoRepository;
        private readonly IIngredienteRepository _ingredienteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse userViewModel;

        public PlatoService(IPlatoRepository platoRepository, IIngredienteRepository ingredienteRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(platoRepository, mapper)
        {
            _ingredienteRepository = ingredienteRepository;
            _platoRepository = platoRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task<List<PlatoViewModel>> GetAllPlatosWithIncludeAsync()
        {
            var platos = await _platoRepository.GetAllWithIncludeAsync();
            var mappedPlatos = _mapper.Map<List<PlatoViewModel>>(platos);
            return mappedPlatos;
        }

        public async Task<SavePlatoViewModel> AddPlatoWithIngredients(SavePlatoViewModel vm)
        {
            var platoEntity = _mapper.Map<Plato>(vm);
            platoEntity = await _platoRepository.AddPlatoWithIngredients(platoEntity, vm.IngredientesIds);
            return _mapper.Map<SavePlatoViewModel>(platoEntity);
        }

        public async Task<PlatoViewModel> GetByIdWithIngredientsViewModelAsync(int id)
        {
            var plato = await _platoRepository.GetByIdWithIngredientsAsync(id);
            if (plato == null)
            {
                return null;
            }

            var platoViewModel = _mapper.Map<PlatoViewModel>(plato);
            return platoViewModel;
        }

        public async Task UpdatePlatoWithIngredientsAsync(SavePlatoViewModel vm, int id)
        {
            var existingPlato = await _platoRepository.GetByIdWithIngredientsAsync(id);
            if (existingPlato == null)
            {
                throw new Exception("Plato no encontrado");
            }

            _mapper.Map(vm, existingPlato);

            existingPlato.Ingredientes.Clear();

            foreach (var ingredientId in vm.IngredientesIds)
            {
                var ingrediente = await _ingredienteRepository.GetByIdAsync(ingredientId);
                if (ingrediente != null)
                {
                    existingPlato.Ingredientes.Add(ingrediente);
                }
                else
                {
                    throw new Exception($"Ingrediente con ID {ingredientId} no encontrado.");
                }
            }

            await _platoRepository.UpdateAsync(existingPlato, existingPlato.Id);
        }

    }
}
