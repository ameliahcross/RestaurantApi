using RestaurantApi.Core.Application.ViewModels.Orden;
using RestaurantApi.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApi.Core.Application.ViewModels.Ingrediente
{
    public class SaveIngredienteViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
