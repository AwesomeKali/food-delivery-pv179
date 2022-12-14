using AutoMapper;
using FoodDelivery.BL.Commands.OrderItemCommands;
using FoodDelivery.BL.Commands.PaymentCardCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.Base;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.EFCore.UnitOfWork.Base;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using FoodDelivery.Shared.Models.MealsModels;
using FoodDelivery.Shared.Models.OrderItemsModels;
using FoodDelivery.Shared.Models.PaymentCardModels;
using MediatR;

namespace FoodDelivery.BL.Handlers.CommandHandlers.OrderItemCommandHandlers;

public class UpdateOrderItemCommandHandler : CommandHandler<UpdateOrderItemCommand, OrderItemDetailModel>, IRequestHandler<UpdateOrderItemCommand, OrderItemDetailModel>
{
    public UpdateOrderItemCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper) : base(unitOfWorkProvider, mapper) { }

    public override async Task<OrderItemDetailModel> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
    {
        var unitOfWork = _unitOfWorkProvider.Create();
        var orderItemEntity = await unitOfWork.OrderItemRepository.GetByIdAsync(request.OrderItemUpdateModel.Id);
        orderItemEntity.Amount = request.OrderItemUpdateModel.Amount;
        unitOfWork.OrderItemRepository.Update(orderItemEntity);
        await unitOfWork.Commit();

        return _mapper.Map<OrderItemDetailModel>(orderItemEntity);
    }
}
