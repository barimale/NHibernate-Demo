using Demo.API.DTOs.Abstraction;
using FluentValidation;

namespace Demo.API.DTOs
{
    public class ProductDto : BaseDto
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public ProductTypeDto Type { get; set; }
        public bool Discontinued { get; set; }
    }

    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(user => user.Category)
                .NotEmpty().WithMessage("Category is required.")
                .Length(2, 50).WithMessage("Category must be between 2 and 50 characters.");
        }
    }
}
