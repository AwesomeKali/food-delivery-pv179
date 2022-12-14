using FoodDelivery.Shared.Models.OrderItemsModels;
using MediatR;

namespace FoodDelivery.BL.Commands.OrderItemCommands;
public record CreateOrderItemCommand(OrderItemCreateModel OrderItemCreateModel) : IRequest<OrderItemDetailModel>;
