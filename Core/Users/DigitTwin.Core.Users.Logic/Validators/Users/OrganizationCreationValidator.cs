using DigitTwin.Lib.Contracts;
using DigitTwin.Lib.Translations;
using FluentValidation;

namespace DigitTwin.Core.Users.Logic.Validators.Users
{
    /// <summary>
    /// Валидатор создания организации
    /// </summary>
    internal class OrganizationCreationValidator : AbstractValidator<OrganizationCreateRequestDto>
    {
        public OrganizationCreationValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage(ValidationMessage.RequiredField(Fields.OrganizationFullName()))
                .MaximumLength(256).WithMessage(ValidationMessage.MaxLengthExceeded(Fields.OrganizationFullName(), 256))
                .MinimumLength(2).WithMessage(ValidationMessage.MinLengthExceeded(Fields.OrganizationFullName(), 2));

            RuleFor(x => x.ShortName)
                .NotEmpty().WithMessage(ValidationMessage.RequiredField(Fields.OrganizationName()))
                .MinimumLength(2).WithMessage(ValidationMessage.MinLengthExceeded(Fields.OrganizationName(), 2))
                .MaximumLength(128).WithMessage(ValidationMessage.MaxLengthExceeded(Fields.OrganizationName(), 128));
        }
    }
}
