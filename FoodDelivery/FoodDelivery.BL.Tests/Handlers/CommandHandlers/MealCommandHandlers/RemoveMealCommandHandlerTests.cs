using FoodDelivery.BL.Commands.FeedbackCommands;
using FoodDelivery.BL.Commands.MealCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.FeedbackCommandHandlers;
using FoodDelivery.BL.Handlers.CommandHandlers.MealCommandHandlers;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.MealCommandHandlers;

public class RemoveMealCommandHandlerTests : IClassFixture<HandlerFixture>
{
    private readonly HandlerFixture _handlerFixture;
    
    public RemoveMealCommandHandlerTests(HandlerFixture handlerFixture)
    {
        _handlerFixture = handlerFixture;
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var id = _handlerFixture.Meals[0].MealEntity.Id;
        
        _handlerFixture.MealRepositoryMock.Setup(m => m.RemoveAsync(It.IsNotIn(id)))
            .Throws(new ArgumentException());
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.MealRepository)
            .Returns(_handlerFixture.MealRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);

        var request = new RemoveMealCommand(id);

        var handler = new RemoveMealCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object);
        
        Assert.True(await handler.Handle(request, new CancellationToken()));
        _handlerFixture.MealRepositoryMock.Verify(m => m.RemoveAsync(It.IsAny<int>()), Times.Once);
    }
}