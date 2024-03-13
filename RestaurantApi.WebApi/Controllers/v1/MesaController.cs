using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Core.Application.Interfaces.Services;
using RestaurantApi.Core.Application.ViewModels.Mesa;

namespace RestaurantApi.WebApi.Controllers.v1
{
    // Heredar de BaseApiController y especificar la versión de la api que estoy usando
    [ApiVersion("1.0")]
    public class MesaController: BaseApiController
    {
        private readonly IMesaService _mesaService;

        public MesaController(IMesaService mesaService)
        {
            _mesaService = mesaService;
        }

        // LISTADO DE MESAS
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









    }
}
