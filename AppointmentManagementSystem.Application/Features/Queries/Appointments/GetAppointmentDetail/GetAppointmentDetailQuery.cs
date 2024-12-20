﻿using AppointmentManagementSystem.Application.DTOs;
using AppointmentManagementSystem.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.Features.Queries.Appointments.GetAppointmentDetail
{
    public class GetAppointmentDetailQuery : IRequest<ResultDto<AppointmentDto>>
    {
        public Guid Id { get; set; }
    }
}
