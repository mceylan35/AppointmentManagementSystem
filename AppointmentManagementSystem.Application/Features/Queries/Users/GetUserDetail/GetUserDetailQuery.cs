using AppointmentManagementSystem.Application.DTOs.Users;
using AppointmentManagementSystem.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.Features.Queries.Users.GetUserDetail
{
    public class GetUserDetailQuery : IRequest<ResultDto<UserDetailDto>>
    {
        public Guid Id { get; set; }
        
    }

}
