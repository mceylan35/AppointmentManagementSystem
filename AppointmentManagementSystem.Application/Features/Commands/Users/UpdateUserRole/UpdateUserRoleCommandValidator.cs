using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.Features.Commands.Users.UpdateUserRole
{
    public class UpdateUserRoleCommandValidator : AbstractValidator<UpdateUserRoleCommand>
    {
        public UpdateUserRoleCommandValidator()
        {
            RuleFor(v => v.UserId)
                .NotEmpty().WithMessage("Kullanıcı ID zorunludur.");

            RuleFor(v => v.RoleId)
                .IsInEnum().WithMessage("Geçerli bir rol seçiniz.");
        }
    }
}