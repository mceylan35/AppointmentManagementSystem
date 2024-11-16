using AppointmentManagementSystem.Application.DTOs.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.Features.Queries.Users.GetUsers
{
    public class GetUsersQuery : IRequest<List<UserDto>>
    {
     
    }
}
