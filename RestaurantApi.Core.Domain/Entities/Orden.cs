using RestaurantApi.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApi.Core.Domain.Entities
{
    public enum StatusOrden
    {
        En_Proceso,
        Completada
    }

    public class Orden : BaseEntity
    {
        public StatusOrden Estado { get; set; }
        public decimal Subtotal { get; set; }
        public ICollection<Plato> Platos { get; set; } = new List<Plato>();
        public string Descripcion { get; set; }

        //Foreing key de la tabla Mesa
        public int MesaId { get; set; }
        // navigation property a Mesa
        public Mesa Mesa { get; set; }
    }
}
