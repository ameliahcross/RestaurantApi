using RestaurantApi.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApi.Core.Application.ViewModels.Mesa
{
    public class SaveMesaViewModel
    {
        public int Id { get; set; }
        public int Capacidad { get; set; }
        public string Descripcion { get; set; }
        public EstadoMesa Estado { get; set; }
    }
}
