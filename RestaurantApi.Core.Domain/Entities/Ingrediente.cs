using RestaurantApi.Core.Domain.Common;

namespace RestaurantApi.Core.Domain.Entities
{
    public class Ingrediente : BaseEntity
    {
        public string Nombre { get; set; }

        // navigation property a Plato
        public ICollection<Plato> Platos { get; set; } = new List<Plato>();
    }
}
