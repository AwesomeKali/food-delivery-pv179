using FoodDelivery.Shared.Models.AddressModels;
using MediatR;

namespace FoodDelivery.BL.Commands.AddressCommands;

public record UpdateOrderAddressCommand(int orderId, AddressUpdateModel AddressUpdateModel) : IRequest<AddressDetailModel>;

