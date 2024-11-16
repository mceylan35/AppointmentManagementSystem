using AppointmentManagementSystem.Application.Common.Interfaces;
using AppointmentManagementSystem.Application.DTOs.Users;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.Features.Queries.Users.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Users.AsQueryable();

            if (!request.IncludeInactive)
            {
                query = query.Where(u => u.IsActive);
            }

            var users = await query
                .OrderBy(u => u.Username)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<UserDto>>(users);
        }
    }
}