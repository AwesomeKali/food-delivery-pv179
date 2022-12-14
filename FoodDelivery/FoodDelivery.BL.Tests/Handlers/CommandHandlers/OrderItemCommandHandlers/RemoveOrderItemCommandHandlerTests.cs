using FoodDelivery.BL.Commands.OrderCommands;
using FoodDelivery.BL.Commands.OrderItemCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.OrderCommandHandlers;
using FoodDelivery.BL.Handlers.CommandHandlers.OrderItemCommandHandlers;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.OrderItemCommandHandlers;

public class RemoveOrderItemCommandHandlerTests : IClassFixture<HandlerFixture>
{
    private readonly HandlerFixture _handlerFixture;
    
    public RemoveOrderItemCommandHandlerTests(HandlerFixture handlerFixture)
    {
        _handlerFixture = handlerFixture;
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var id = _handlerFixture.OrderItems[0].OrderItemEntity.Id;
        
        _handlerFixture.OrderItemRepositoryMock.Setup(o => o.RemoveAsync(It.IsNotIn(id)))
            .Throws(new ArgumentException());
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.OrderItemRepository)
            .Returns(_handlerFixture.OrderItemRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);

        var request = new RemoveOrderItemCommand(id);

        var handler = new RemoveOrderItemCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object);
        
        Assert.True(await handler.Handle(request, new CancellationToken()));
        _handlerFixture.OrderItemRepositoryMock.Verify(o => o.RemoveAsync(It.IsAny<int>()), Times.Once);
    }
}