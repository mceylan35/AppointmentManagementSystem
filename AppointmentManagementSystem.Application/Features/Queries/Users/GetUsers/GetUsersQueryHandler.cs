using AppointmentManagementSystem.Application.Common.Interfaces;
using AppointmentManagementSystem.Application.DTOs.Users;
using AppointmentManagementSystem.Domain.Common;
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
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ResultDto<List<UserDto>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultDto<List<UserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Users.AsQueryable();
             
            query = query.Where(i => i.IsActive);
            query= query.Where(i => !i.IsDeleted);

            var users = await query
                .OrderBy(u => u.Username)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var response= _mapper.Map<List<UserDto>>(users);
            return ResultDto<List<UserDto>>.Success(response, "İşlem Başarılı");
        }
    }
}