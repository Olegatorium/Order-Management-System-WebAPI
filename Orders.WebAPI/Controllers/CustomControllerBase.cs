using Microsoft.AspNetCore.Mvc;

namespace Orders.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/Orders")]
    public class CustomControllerBase : ControllerBase
    {
    }
}
