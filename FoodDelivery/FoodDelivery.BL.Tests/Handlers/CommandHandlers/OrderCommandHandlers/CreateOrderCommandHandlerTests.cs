using FoodDelivery.BL.Commands.MealCommands;
using FoodDelivery.BL.Commands.OrderCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.MealCommandHandlers;
using FoodDelivery.BL.Handlers.CommandHandlers.OrderCommandHandlers;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.MealsModels;
using FoodDelivery.Shared.Models.OrderModels;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.OrderCommandHandlers;

public class CreateOrderCommandHandlerTests : IClassFixture<HandlerFixture>
{
    private readonly HandlerFixture _handlerFixture;
    
    public CreateOrderCommandHandlerTests(HandlerFixture handlerFixture)
    {
        _handlerFixture = handlerFixture;
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var entity = _handlerFixture.Orders[0].OrderEntity;
        var createModel = _handlerFixture.Orders[0].OrderCreateModel;
        var detailModel = _handlerFixture.Orders[0].OrderDetailModel;
        
        _handlerFixture.OrderRepositoryMock.Setup(o => o.Insert(It.IsAny<OrderEntity>()));
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.OrderRepository)
            .Returns(_handlerFixture.OrderRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);
        _handlerFixture.MapperMock.Setup(m => m.Map<OrderEntity>(createModel))
            .Returns(entity);
        _handlerFixture.MapperMock.Setup(m => m.Map<OrderDetailModel>(entity))
            .Returns(detailModel);

        var request = new CreateOrderCommand(createModel);

        var handler = new CreateOrderCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object);
        
        Assert.True(await handler.Handle(request, new CancellationToken()) == detailModel);
    }
}