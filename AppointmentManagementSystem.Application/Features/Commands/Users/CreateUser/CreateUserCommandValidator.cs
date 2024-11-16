using AppointmentManagementSystem.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.Features.Commands.Users.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateUserCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Username)
                .NotEmpty().WithMessage("Kullanıcı adı zorunludur.")
                .MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakter olmalıdır.")
                .MustAsync(BeUniqueUsername).WithMessage("Bu kullanıcı adı zaten kullanılıyor.");

            RuleFor(v => v.Email)
                .NotEmpty().WithMessage("E-posta adresi zorunludur.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
                .MustAsync(BeUniqueEmail).WithMessage("Bu e-posta adresi zaten kullanılıyor.");

            RuleFor(v => v.Password)
                .NotEmpty().WithMessage("Şifre zorunludur.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");
        }

        private async Task<bool> BeUniqueUsername(string username, CancellationToken cancellationToken)
        {
            return !await _context.Users.AnyAsync(u => u.Username == username);
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return !await _context.Users.AnyAsync(u => u.Email == email);
        }
    }
}
