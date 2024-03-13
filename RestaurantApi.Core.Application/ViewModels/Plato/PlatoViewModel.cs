using RestaurantApi.Core.Application.ViewModels;
using RestaurantApi.Core.Application.ViewModels.Ingrediente;
using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Core.Application.ViewModels.Plato
{
    public class PlatoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Porcion { get; set; }
        public CategoriaPlato Categoria { get; set; }
        public ICollection<IngredienteViewModel> Ingredientes { get; set; } = new List<IngredienteViewModel>();
    }
}
