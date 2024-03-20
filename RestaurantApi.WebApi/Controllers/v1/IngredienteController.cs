using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Core.Application.Interfaces.Services;
using RestaurantApi.Core.Application.ViewModels.Ingrediente;

namespace RestaurantApi.WebApi.Controllers.v1
{
    // Heredar de BaseApiController y especificar la versión de la api que estoy usando
    [ApiVersion("1.0")]
    [Authorize (Roles = "Admin")]
    public class IngredienteController : BaseApiController
    {
        private readonly IIngredienteService _ingredienteService;

        public IngredienteController(IIngredienteService ingredienteService)
        {
            _ingredienteService = ingredienteService;
        }


        // LISTADO DE INGREDIENTES
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IngredienteViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List()
        {
            try
            {
                var ingredientes = await _ingredienteService.GetAllViewModel();
                if (ingredientes == null || ingredientes.Count == 0)
                {
                    return NoContent();
                }
                return Ok(ingredientes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // OBTENER INGREDIENTE POR ID
        [HttpGet("{id}")] // hay que indicar que la URL debe venir con el id a buscar
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveIngredienteViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var ingrediente = await _ingredienteService.GetByIdSaveViewModel(id);
                if (ingrediente == null)
                {
                    return NoContent();
                }
                return Ok(ingrediente); // el Ok() es utilizado solo cuando se devuelven datos
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // CREAR UN INGREDIENTE
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(SaveIngredienteViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var newIngredient = await _ingredienteService.Add(vm);
                return CreatedAtAction(nameof(GetById), new { id = newIngredient.Id }, newIngredient); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // ACTUALIZAR UN INGREDIENTE
        [HttpPut("{id}")]  // hay que indicar que la URL debe venir con el id a actualizar
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, SaveIngredienteViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _ingredienteService.Update(vm, id);
                return Ok(vm);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }


        }









    }
}
