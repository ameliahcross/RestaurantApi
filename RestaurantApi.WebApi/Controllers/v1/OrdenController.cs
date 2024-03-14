using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Core.Application.Interfaces.Services;
using RestaurantApi.Core.Application.ViewModels.Mesa;
using RestaurantApi.Core.Application.ViewModels.Orden;

namespace RestaurantApi.WebApi.Controllers.v1
{
    // Heredar de BaseApiController y especificar la versión de la api que estoy usando
    [ApiVersion("1.0")]
    public class OrdenController: BaseApiController
    {
        private readonly IOrdenService _ordenService;

        public OrdenController(IOrdenService ordenService)
        {
            _ordenService = ordenService;
        }

        // LISTADO DE ORDENES
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrdenViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List()
        {
            try
            {
                var ordenes = await _ordenService.GetAllWithIncludeAsync();
                if (ordenes == null || ordenes.Count == 0)
                {
                    return NoContent();
                }
                return Ok(ordenes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // OBTENER ORDEN POR ID
        [HttpGet("{id}")] // hay que indicar que la URL debe venir con el id a buscar
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrdenViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var orden = await _ordenService.GetOrdenByIdWithIncludeAsync(id);
                if (orden == null)
                {
                    return NoContent();
                }
                return Ok(orden); // el Ok() es utilizado solo cuando se devuelven datos
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // CREAR UNA ORDEN
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(SaveOrdenViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var newOrden = await _ordenService.AddOrdenWithPlatos(vm); 
                return CreatedAtAction(nameof(GetById), new { id = newOrden.Id }, newOrden);
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

        // ACTUALIZAR UNA ORDEN
        [HttpPut("{id}")]  // hay que indicar que la URL debe venir con el id a actualizar
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, UpdateOrdenViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var updatedOrden = await _ordenService.UpdateOrden(vm, id);
                return Ok(updatedOrden);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        // ELIMINAR UNA ORDEN
        [HttpDelete("{id}")]  // hay que indicar que la URL debe venir con el id a actualizar
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _ordenService.DeleteOrdenWithPlatos(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }









    }
}
