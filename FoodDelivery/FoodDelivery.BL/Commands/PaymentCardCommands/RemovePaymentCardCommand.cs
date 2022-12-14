using MediatR;

namespace FoodDelivery.BL.Commands.PaymentCardCommands;

public record RemovePaymentCardCommand(int Id) : IRequest<bool>;

