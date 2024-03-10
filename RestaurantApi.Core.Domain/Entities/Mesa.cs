using RestaurantApi.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
