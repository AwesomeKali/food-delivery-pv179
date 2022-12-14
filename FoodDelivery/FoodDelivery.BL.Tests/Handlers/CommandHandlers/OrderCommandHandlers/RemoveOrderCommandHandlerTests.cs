using FoodDelivery.BL.Commands.MealCommands;
using FoodDelivery.BL.Commands.OrderCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.MealCommandHandlers;
using FoodDelivery.BL.Handlers.CommandHandlers.OrderCommandHandlers;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.OrderCommandHandlers;

public class RemoveOrderCommandHandlerTests : IClassFixture<HandlerFixture>
{
    private readonly HandlerFixture _handlerFixture;
    
    public RemoveOrderCommandHandlerTests(HandlerFixture handlerFixture)
    {
        _handlerFixture = handlerFixture;
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var id = _handlerFixture.Orders[0].OrderEntity.Id;
        
        _handlerFixture.OrderRepositoryMock.Setup(o => o.RemoveAsync(It.IsNotIn(id)))
            .Throws(new ArgumentException());
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.OrderRepository)
            .Returns(_handlerFixture.OrderRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);

        var request = new RemoveOrderCommand(id);

        var handler = new RemoveOrderCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object);
        
        Assert.True(await handler.Handle(request, new CancellationToken()));
        _handlerFixture.OrderRepositoryMock.Verify(o => o.RemoveAsync(It.IsAny<int>()), Times.Once);
    }
}