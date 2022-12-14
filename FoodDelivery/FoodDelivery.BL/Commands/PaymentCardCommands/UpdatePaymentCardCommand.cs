using FoodDelivery.Shared.Models.PaymentCardModels;
using MediatR;

namespace FoodDelivery.BL.Commands.PaymentCardCommands;

public record UpdatePaymentCardCommand(PaymentCardUpdateModel PaymentCardUpdateModel) : IRequest<PaymentCardDetailModel>;

