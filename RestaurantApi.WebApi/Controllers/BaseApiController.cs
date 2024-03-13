using Microsoft.AspNetCore.Mvc;

namespace RestaurantApi.WebApi.Controllers
{
    [ApiController] // Los API controllers no retornan vistas, sino información en JSON
    [Route("api/v{version:apiVersion}/[controller]")]

    // Hacer clase abstracta para que de ella solo se pueda heredar
    public abstract class BaseApiController : ControllerBase
    {
        
    }
}
