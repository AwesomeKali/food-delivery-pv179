using FoodDelivery.BL.Commands.MealCommands;
using FoodDelivery.BL.Commands.OrderItemCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.MealCommandHandlers;
using FoodDelivery.BL.Handlers.CommandHandlers.OrderItemCommandHandlers;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.MealsModels;
using FoodDelivery.Shared.Models.OrderItemsModels;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.OrderItemCommandHandlers;

public class CreateOrderItemCommandHandlerTests : IClassFixture<HandlerFixture>
{
    private readonly HandlerFixture _handlerFixture;
    
    public CreateOrderItemCommandHandlerTests(HandlerFixture handlerFixture)
    {
        _handlerFixture = handlerFixture;
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var entity = _handlerFixture.OrderItems[0].OrderItemEntity;
        var createModel = _handlerFixture.OrderItems[0].OrderItemCreateModel;
        var detailModel = _handlerFixture.OrderItems[0].OrderItemDetailModel;
        
        _handlerFixture.OrderItemRepositoryMock.Setup(o => o.Insert(It.IsAny<OrderItemEntity>()));
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.OrderItemRepository)
            .Returns(_handlerFixture.OrderItemRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);
        _handlerFixture.MapperMock.Setup(m => m.Map<OrderItemEntity>(createModel))
            .Returns(entity);
        _handlerFixture.MapperMock.Setup(m => m.Map<OrderItemDetailModel>(entity))
            .Returns(detailModel);

        var request = new CreateOrderItemCommand(createModel);

        var handler = new CreateOrderItemCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object);
        
        Assert.True(await handler.Handle(request, new CancellationToken()) == detailModel);
    }
}