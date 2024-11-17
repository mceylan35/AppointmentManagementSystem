using AppointmentManagementSystem.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentManagementSystem.WebApp.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class ServicesController : BaseController
    {
        private readonly IServiceService _serviceService;

        public ServicesController(IMediator mediator, IServiceService serviceService)
        { 
            _serviceService = serviceService;
        }

       

        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            var services = await _serviceService.GetAllActiveServicesAsync();
            return Json(services);
        }
    }
}
