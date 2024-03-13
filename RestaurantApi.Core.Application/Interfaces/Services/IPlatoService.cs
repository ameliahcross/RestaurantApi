using RestaurantApi.Core.Application.ViewModels.Plato;
using RestaurantApi.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApi.Core.Application.Interfaces.Services
{
    public interface IPlatoService : IGenericService<SavePlatoViewModel, PlatoViewModel, Plato>
    {
        Task<List<PlatoViewModel>> GetAllPlatosWithIncludeAsync();
        Task<SavePlatoViewModel> AddPlatoWithIngredients(SavePlatoViewModel vm);
        Task<PlatoViewModel> GetByIdWithIngredientsViewModelAsync(int id);
        Task UpdatePlatoWithIngredientsAsync(SavePlatoViewModel vm, int id);
    }
}
