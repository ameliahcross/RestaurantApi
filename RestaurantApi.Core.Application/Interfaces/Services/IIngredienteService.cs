using RestaurantApi.Core.Application.ViewModels.Ingrediente;
using RestaurantApi.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApi.Core.Application.Interfaces.Services
{
    public interface IIngredienteService : IGenericService<SaveIngredienteViewModel, IngredienteViewModel, Ingrediente>
    {
        Task<List<IngredienteViewModel>> GetAllIngredientsWithIncludeAsync();
    }
}
