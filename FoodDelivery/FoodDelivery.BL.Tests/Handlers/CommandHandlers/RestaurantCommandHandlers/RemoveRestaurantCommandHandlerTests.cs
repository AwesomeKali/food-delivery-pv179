using FoodDelivery.BL.Commands.OrderCommands;
using FoodDelivery.BL.Commands.RestaurantCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.OrderCommandHandlers;
using FoodDelivery.BL.Handlers.CommandHandlers.RestaurantCommandHandlers;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.RestaurantCommandHandlers;

public class RemoveRestaurantCommandHandlerTests : IClassFixture<HandlerFixture>
{
    private readonly HandlerFixture _handlerFixture;
    
    public RemoveRestaurantCommandHandlerTests(HandlerFixture handlerFixture)
    {
        _handlerFixture = handlerFixture;
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var id = _handlerFixture.Restaurants[0].RestaurantEntity.Id;
        
        _handlerFixture.RestaurantRepositoryMock.Setup(r => r.RemoveAsync(It.IsNotIn(id)))
            .Throws(new ArgumentException());
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.RestaurantRepository)
            .Returns(_handlerFixture.RestaurantRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);

        var request = new RemoveRestaurantCommand(id);

        var handler = new RemoveRestaurantCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object);
        
        Assert.True(await handler.Handle(request, new CancellationToken()));
        _handlerFixture.RestaurantRepositoryMock.Verify(r => r.RemoveAsync(It.IsAny<int>()), Times.Once);
    }
}