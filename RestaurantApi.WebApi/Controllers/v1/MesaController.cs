using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Core.Application.Interfaces.Services;
using RestaurantApi.Core.Application.ViewModels.Mesa;
using RestaurantApi.Core.Application.ViewModels.Orden;
using RestaurantApi.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace RestaurantApi.WebApi.Controllers.v1
{
    // Heredar de BaseApiController y especificar la versión de la api que estoy usando
    [ApiVersion("1.0")]
    public class MesaController: BaseApiController
    {
        private readonly IMesaService _mesaService;
        private readonly IOrdenService _ordenService;

        public MesaController(IMesaService mesaService, IOrdenService ordenService)
        {
            _mesaService = mesaService;
            _ordenService = ordenService;
        }

        // LISTADO DE MESAS
        [Authorize(Roles = "Admin, Mesero")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MesaViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List()
        {
            try
            {
                var mesas = await _mesaService.GetAllViewModel();
                if (mesas == null || mesas.Count == 0)
                {
                    return NoContent();
                }
                return Ok(mesas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // OBTENER MESA POR ID
        [Authorize(Roles = "Admin, Mesero")]
        [HttpGet("{id}")] // hay que indicar que la URL debe venir con el id a buscar
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveMesaViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var mesa = await _mesaService.GetByIdSaveViewModel(id);
                if (mesa == null)
                {
                    return NoContent();
                }
                return Ok(mesa); // el Ok() es utilizado solo cuando se devuelven datos
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // CREAR UNA MESA
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(SaveMesaViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var newMesa = await _mesaService.Add(vm); 
                return CreatedAtAction(nameof(GetById), new { id = newMesa.Id }, newMesa);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // ACTUALIZAR UNA MESA
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]  // hay que indicar que la URL debe venir con el id a actualizar
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, SaveMesaViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _mesaService.Update(vm, id);
                return Ok(vm);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // OBTENER LA ORDEN DE LA MESA
        [Authorize(Roles = "Mesero")]
        [HttpGet("TableOrder/{tableId}")]
        [SwaggerOperation (Summary = "Obtener la orden de la mesa")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrdenViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTableOrder(int tableId)
        {
            try
            {
                var tableOrder = await _ordenService.GetOrdenByIdTableId(tableId);
                if (tableOrder == null)
                {
                    return NoContent();
                }
                return Ok(tableOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        // CAMBIAR ESTATUS DE LA MESA
        [Authorize(Roles = "Mesero")]
        [HttpPatch("ChangeStatus/{mesaId}")] //hay que indicar que la URL debe venir con el id a buscar
        [SwaggerOperation(Summary = "Cambiar estatus de la mesa")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MesaViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeStatus(int mesaId, [FromBody] EstadoMesa newStatus)
        {
            try
            {
                var updatedMesa = await _mesaService.ChangeMesaStatusAsync(mesaId, newStatus);
                if (updatedMesa == null)
                {
                    return NoContent();
                }
                return Ok(updatedMesa);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }










    }
}
