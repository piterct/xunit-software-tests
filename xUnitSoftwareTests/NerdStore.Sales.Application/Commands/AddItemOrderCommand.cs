using FluentValidation;
using NerdStore.Core.Messages;
using NerdStore.Sales.Domain;

namespace NerdStore.Sales.Application.Commands
{
    public class AddItemOrderCommand : Command
    {
        public Guid ClientId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitValue { get; set; }

        public AddItemOrderCommand(Guid clientId, Guid productId, string name, int quantity, decimal unitValue)
        {
            ClientId = clientId;
            ProductId = productId;
            Name = name;
            Quantity = quantity;
            UnitValue = unitValue;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddItemOrderCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
       
    }

    public class AddItemOrderCommandValidation : AbstractValidator<AddItemOrderCommand>
    {
        public static string ClientIdErrorMsg => "Client id is invalid";
        public static string ProductIdErrorMsg => "Product id is invalid";
        public static string NameErrorMsg => "The name of product was not sended";
        public static string QtdMaxErrorMsg => $"The maximum of quantity is {Order.MAX_UNITS_ITEM}";
        public static string QtdMinErrorMsg => "The minimum of quantity is 1";
        public static string ValueErrorMsg => "The value of product must be greater than zero";

        public AddItemOrderCommandValidation()
        {
            RuleFor(c => c.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage(ClientIdErrorMsg);

            RuleFor(c => c.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage(ProductIdErrorMsg);

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage(NameErrorMsg);

            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage(QtdMinErrorMsg)
                .LessThanOrEqualTo(Order.MAX_UNITS_ITEM)
                .WithMessage(QtdMaxErrorMsg);

            RuleFor(c => c.UnitValue)
                .GreaterThan(0)
                .WithMessage(ValueErrorMsg);
        }
    }
}
