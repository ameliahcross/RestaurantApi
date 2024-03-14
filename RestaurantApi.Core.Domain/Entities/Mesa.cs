using RestaurantApi.Core.Domain.Common;

namespace RestaurantApi.Core.Domain.Entities
{
    public enum EstadoMesa
    {
        Disponible,
        Proceso_Atencion,
        Atendida
    }

    public class Mesa : BaseEntity
    {
        public int Capacidad { get; set; }
        public string Descripcion { get; set; }
        public EstadoMesa Estado { get; set; }
    }
}
