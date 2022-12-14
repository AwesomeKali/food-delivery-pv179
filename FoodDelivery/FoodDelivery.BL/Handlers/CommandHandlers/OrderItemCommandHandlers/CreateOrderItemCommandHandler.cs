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

public class CreateOrderItemCommandHandler : CommandHandler<CreateOrderItemCommand, OrderItemDetailModel>, IRequestHandler<CreateOrderItemCommand, OrderItemDetailModel>
{
    public CreateOrderItemCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper) : base(unitOfWorkProvider, mapper) { }

    public override async Task<OrderItemDetailModel> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
    {
        var orderItemEntity = _mapper.Map<OrderItemEntity>(request.OrderItemCreateModel);

        var unitOfWork = _unitOfWorkProvider.Create();
        if (await unitOfWork.OrderRepository.GetByIdAsync(request.OrderItemCreateModel.OrderId) == null ||
            await unitOfWork.MealRepository.GetByIdAsync(request.OrderItemCreateModel.MealId) == null)
        {
            return null;
        }
        unitOfWork.OrderItemRepository.Insert(orderItemEntity);
        await unitOfWork.Commit();

        return _mapper.Map<OrderItemDetailModel>(orderItemEntity);
    }
}
