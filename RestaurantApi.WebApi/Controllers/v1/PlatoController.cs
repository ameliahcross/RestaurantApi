using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Core.Application.Interfaces.Services;
using RestaurantApi.Core.Application.ViewModels.Plato;

namespace RestaurantApi.WebApi.Controllers.v1
{
    // Heredar de BaseApiController y especificar la versión de la api que estoy usando
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin")]
    public class PlatoController : BaseApiController
    {
        private readonly IPlatoService _platoService;

        public PlatoController(IPlatoService platoService)
        {
            _platoService = platoService;
        }

        // LISTADO DE PLATOS
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlatoViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List()
        {
            try
            {
                var platos = await _platoService.GetAllPlatosWithIncludeAsync();
                if (platos == null || platos.Count == 0)
                {
                    return NoContent();
                }
                return Ok(platos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // OBTENER PLATO POR ID
        [HttpGet("{id}")] // hay que indicar que la URL debe venir con el id a buscar
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SavePlatoViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var plato = await _platoService.GetByIdWithIngredientsViewModelAsync(id);
                if (plato == null)
                {
                    return NoContent();
                }
                return Ok(plato); // el Ok() es utilizado solo cuando se devuelven datos
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // CREAR UN PLATO
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(SavePlatoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var newPlato = await _platoService.AddPlatoWithIngredients(vm);
                return CreatedAtAction(nameof(GetById), new { id = newPlato.Id }, newPlato);
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

        // ACTUALIZAR UN PLATO
        [HttpPut("{id}")]  // hay que indicar que la URL debe venir con el id a actualizar
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, SavePlatoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _platoService.UpdatePlatoWithIngredientsAsync(vm, id);
                return Ok(vm);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }


        }









    }
}
