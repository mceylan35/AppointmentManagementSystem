using AppointmentManagementSystem.Application.DTOs;
using AppointmentManagementSystem.Application.Features.Commands.Appointments.CreateAppointment;
using AppointmentManagementSystem.Application.Features.Commands.Appointments.DeleteAppointment;
using AppointmentManagementSystem.Application.Features.Commands.Appointments.UpdateAppointment;
using AppointmentManagementSystem.Application.Features.Queries.Appointments.GetAppointmentDetail;
using AppointmentManagementSystem.Application.Features.Queries.Appointments.GetAppointments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentManagementSystem.WebApp.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class AppointmentController : BaseController
    {

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("appointments")]
        public async Task<ActionResult<List<AppointmentDto>>> GetAppointments()
        {
            return Ok(await Mediator.Send(new GetAppointmentsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDto>> GetAppointment(Guid id)
        {
            
            return Ok(await Mediator.Send(new GetAppointmentDetailQuery { Id = id }));
        }

        [Authorize(Roles = "User")]
        [HttpPost("create")]
        public async Task<ActionResult<int>> Create([FromBody]CreateAppointmentCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] UpdateAppointmentCommand command)
        {
            
            return Ok(await Mediator.Send(command));
        }

        [Authorize(Roles = "User")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
           return Ok(await Mediator.Send(new DeleteAppointmentCommand { Id = id }));
            
        }
    }
}
