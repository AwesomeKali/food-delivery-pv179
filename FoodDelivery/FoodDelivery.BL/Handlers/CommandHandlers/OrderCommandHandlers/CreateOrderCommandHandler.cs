using AutoMapper;
using FoodDelivery.BL.Commands.MealCommands;
using FoodDelivery.BL.Commands.OrderCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.Base;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.EFCore.UnitOfWork.Base;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using FoodDelivery.Shared.Models.MealsModels;
using FoodDelivery.Shared.Models.OrderModels;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.BL.Handlers.CommandHandlers.OrderCommandHandlers;

public class CreateOrderCommandHandler : CommandHandler<CreateOrderCommand, OrderListModel>, IRequestHandler<CreateOrderCommand, OrderListModel>
{
    private readonly UserManager<UserEntity> _userManager;

    public CreateOrderCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper,
        UserManager<UserEntity> userManager) : base(unitOfWorkProvider, mapper)
    {
        _userManager = userManager;
    }

    public override async Task<OrderListModel> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.GetUserAsync(request.User);
        var address = new AddressEntity
        {
            Street = "",
            Number = "",
            City = "",
            PostalCode = ""
        };
        var order = new OrderEntity
        {
            User = user,
            Address = address,
            RestaurantId = request.RestaurantId
        };

        var unitOfWork = _unitOfWorkProvider.Create();
        unitOfWork.OrderRepository.Insert(order);
        await unitOfWork.Commit();

        return _mapper.Map<OrderListModel>(order);
    }
}
