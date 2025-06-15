using Demo.API.DTOs.Abstraction;
using FluentValidation;

namespace Demo.API.DTOs
{
    /// <summary>
    /// Data Transfer Object for representing a product.
    /// Inherits common properties from <see cref="BaseDto"/>.
    /// </summary>
    public class ProductDto : BaseDto
    {
        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the category of the product.
        /// </summary>
        public required string Category { get; set; }

        /// <summary>
        /// Gets or sets the type of the product.
        /// </summary>
        public required ProductTypeDto Type { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the product is discontinued.
        /// </summary>
        public required bool Discontinued { get; set; }
    }

    /// <summary>
    /// Validator for <see cref="ProductDto"/> using FluentValidation.
    /// </summary>
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(user => user.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 50).WithMessage("Name must be between 2 and 50 characters.");

            RuleFor(user => user.Category)
                .NotEmpty().WithMessage("Category is required.")
                .Length(2, 50).WithMessage("Category must be between 2 and 50 characters.");

            RuleFor<ProductTypeDto>(user => user.Type)
                .NotNull().WithMessage("Type is required.");

            RuleFor(user => user.Discontinued)
                .NotNull().WithMessage("Discontinued status is required.")
                .Must(x => x == true || x == false).WithMessage("Discontinued must be a boolean value.");
        }
    }
}