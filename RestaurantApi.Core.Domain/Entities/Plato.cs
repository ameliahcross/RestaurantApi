using RestaurantApi.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApi.Core.Domain.Entities
{
    public enum CategoriaPlato
    {
        Entrada,
        Plato_Fuerte,
        Postre,
        Bebida
    }
    public class Plato : BaseEntity
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Porcion { get; set; }
        public CategoriaPlato Categoria { get; set; }
        public ICollection<Ingrediente> Ingredientes { get; set;} = new List<Ingrediente>();

        // navigation property a Orden
        public ICollection<Orden> Ordenes { get; set; } = new List<Orden>(); 
    }
}
