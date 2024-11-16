using AppointmentManagementSystem.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.Features.Commands.Users.DeleteUser
{
    public class DeleteUserCommand : IRequest<ResultDto<bool>>
    {
        public Guid Id { get; set; }
    }
}
