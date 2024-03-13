using RestaurantApi.Core.Application.ViewModels.Ingrediente;
using RestaurantApi.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApi.Core.Application.ViewModels.Plato
{
    public class SavePlatoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Porcion { get; set; }
        public CategoriaPlato Categoria { get; set; }
        public ICollection<int> IngredientesIds { get; set; } = new List<int>();
    }
}
