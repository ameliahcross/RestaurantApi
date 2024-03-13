using RestaurantApi.Core.Application.ViewModels;
using RestaurantApi.Core.Application.ViewModels.Plato;
using RestaurantApi.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApi.Core.Application.ViewModels.Ingrediente
{
    public class IngredienteViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
