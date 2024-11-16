using MediatR;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentManagementSystem.WebApp.Controllers
{
   
    [Route("[controller]")]
    public abstract class BaseController : Controller
    {
        private ISender _mediator = null!;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}