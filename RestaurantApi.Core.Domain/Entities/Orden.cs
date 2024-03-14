using RestaurantApi.Core.Domain.Common;

namespace RestaurantApi.Core.Domain.Entities
{
    public enum StatusOrden
    {
        En_Proceso,
        Completada
    }

    public class Orden : BaseEntity
    {
        public StatusOrden Estado { get; set; } = StatusOrden.En_Proceso;
        public decimal Subtotal { get; set; }
        public ICollection<Plato> Platos { get; set; } = new List<Plato>();
        public string Descripcion { get; set; }

        //Foreing key de la tabla Mesa
        public int MesaId { get; set; }
        // navigation property a Mesa
        public Mesa Mesa { get; set; }
      
    }
}
