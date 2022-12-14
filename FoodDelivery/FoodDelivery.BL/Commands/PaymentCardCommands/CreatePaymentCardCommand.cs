using FoodDelivery.Shared.Models.PaymentCardModels;
using MediatR;

namespace FoodDelivery.BL.Commands.PaymentCardCommands;

public record CreatePaymentCardCommand(PaymentCardCreateModel PaymentCardCreateModel) : IRequest<PaymentCardDetailModel>;
