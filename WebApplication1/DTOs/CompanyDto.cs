using Demo.API.DTOs.Abstraction;
using FluentValidation;

namespace Demo.API.DTOs
{
    public class CompanyDto: BaseDto
    {
        public virtual string Foo { get; set; }
        public virtual IList<AddressDto> Addresses { get; set; } = new List<AddressDto>();
    }

    public class CompanyValidator : AbstractValidator<CompanyDto>
    {
        public CompanyValidator()
        {
            RuleFor(user => user.Foo)
                .NotEmpty().WithMessage("Foo is required.")
                .Length(2, 50).WithMessage("Foo must be between 2 and 50 characters.");
        }
    }
}
