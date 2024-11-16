using AppointmentManagementSystem.Application.DTOs.Users;
using AppointmentManagementSystem.Application.DTOs;
using AppointmentManagementSystem.Application.Features.Commands.Users.CreateUser;
using AppointmentManagementSystem.Application.Features.Queries.Appointments.GetAppointments;
using AppointmentManagementSystem.Application.Features.Queries.Users.GetUsers;
using Microsoft.AspNetCore.Mvc;
using AppointmentManagementSystem.Application.Features.Commands.Users.DeleteUser;
using AppointmentManagementSystem.Application.Features.Commands.Users.UpdateUser;
using AppointmentManagementSystem.Application.Features.Queries.Users.GetUserDetail;
using Microsoft.AspNetCore.Authorization;

namespace AppointmentManagementSystem.WebApp.Controllers
{
    [Authorize("Admin")]
    public class UsersController : BaseController
    {
        public UsersController()
        {
            
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
         

        [HttpGet("getusers")]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            return Ok(await Mediator.Send(new GetUsersQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(Guid id)
        {
            return Ok(await Mediator.Send(new GetUserDetailQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateUser([FromBody] CreateUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand command)
        {
           
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        { 
            return Ok(await Mediator.Send(new DeleteUserCommand { Id = id }));
        }

      
    }
}