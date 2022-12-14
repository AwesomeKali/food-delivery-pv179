using FoodDelivery.BL.Commands.MealCommands;
using FoodDelivery.BL.Commands.OrderItemCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.MealCommandHandlers;
using FoodDelivery.BL.Handlers.CommandHandlers.OrderItemCommandHandlers;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.MealsModels;
using FoodDelivery.Shared.Models.OrderItemsModels;
using FoodDelivery.Shared.Models.OrderModels;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.OrderItemCommandHandlers;

public class UpdateOrderItemCommandHandlerTests : IClassFixture<HandlerFixture>
{
    private readonly HandlerFixture _handlerFixture;
    
    public UpdateOrderItemCommandHandlerTests(HandlerFixture handlerFixture)
    {
        _handlerFixture = handlerFixture;
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var entity = _handlerFixture.OrderItems[0].OrderItemEntity;
        var updateModel = _handlerFixture.OrderItems[0].OrderItemUpdateModel;
        var detailModel = _handlerFixture.OrderItems[0].OrderItemDetailModel;
        
        _handlerFixture.OrderItemRepositoryMock.Setup(o => o.Update(It.IsNotIn(entity)))
            .Throws(new ArgumentException());
        _handlerFixture.MapperMock.Setup(m => m.Map<OrderItemEntity>(It.IsAny<OrderItemUpdateModel>()))
            .Returns(entity);
        _handlerFixture.MapperMock.Setup(m => m.Map<OrderItemDetailModel>(It.IsAny<OrderItemEntity>()))
            .Returns(detailModel);
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.OrderItemRepository)
            .Returns(_handlerFixture.OrderItemRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);

        var request = new UpdateOrderItemCommand(updateModel);

        var handler = new UpdateOrderItemCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object);
        
        Assert.True(await handler.Handle(request, new CancellationToken()) == detailModel);
        _handlerFixture.OrderItemRepositoryMock.Verify(o => o.Update(It.IsAny<OrderItemEntity>()), Times.Once);
    }
}