using AppointmentManagementSystem.Application.DTOs;
using AppointmentManagementSystem.Application.DTOs.Users;
using AppointmentManagementSystem.Application.Features.Commands.Users.UpdateUserRole;
using AppointmentManagementSystem.Application.Features.Queries.Appointments.GetAppointments; 
using AppointmentManagementSystem.Application.Features.Queries.Users.GetUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentManagementSystem.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        [HttpGet("users")]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            return await Mediator.Send(new GetUsersQuery());
        }

        [HttpPut("users/{id}/role")]
        public async Task<ActionResult> UpdateUserRole(Guid id, UpdateUserRoleCommand command)
        {
            if (id != command.UserId)
                return BadRequest();

            await Mediator.Send(command);
            return NoContent();
        }

        [HttpGet("appointments")]
        public async Task<ActionResult<List<AppointmentDto>>> GetAllAppointments()
        {
            return await Mediator.Send(new GetAppointmentsQuery { IncludeAll = true });
        }
    }
}
