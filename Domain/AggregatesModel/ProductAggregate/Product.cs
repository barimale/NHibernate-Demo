using Demo.Domain.Abstraction;
using FluentValidation;

namespace Demo.Domain.AggregatesModel.ProductAggregate
{
    public class Product: Entity<int>, IAggregateRoot
    {
        public virtual string Name { get; set; }
        public virtual string Category { get; set; }
        public virtual bool Discontinued { get; set; }
        public virtual ProductType Type { get; set; }   
    }

    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(user => user.Category)
                .NotEmpty().WithMessage("Category is required.")
                .Length(2, 50).WithMessage("Category must be between 2 and 50 characters.");
        }
    }
}
