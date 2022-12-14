using FoodDelivery.BL.Commands.AddressCommands;
using FoodDelivery.BL.Commands.FeedbackCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.AddressCommandHandlers;
using FoodDelivery.BL.Handlers.CommandHandlers.FeedbackCommandHandlers;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.FeedbackCommandHandlers;

public class RemoveFeedbackCommandHandlerTests : IClassFixture<HandlerFixture>
{
    private readonly HandlerFixture _handlerFixture;
    
    public RemoveFeedbackCommandHandlerTests(HandlerFixture handlerFixture)
    {
        _handlerFixture = handlerFixture;
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var id = _handlerFixture.Feedbacks[0].FeedbackEntity.Id;
        
        _handlerFixture.FeedbackRepositoryMock.Setup(f => f.RemoveAsync(It.IsNotIn(id)))
            .Throws(new ArgumentException());
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.FeedbackRepository)
            .Returns(_handlerFixture.FeedbackRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);

        var request = new RemoveFeedbackCommand(id);

        var handler = new RemoveFeedbackCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object);
        
        Assert.True(await handler.Handle(request, new CancellationToken()));
        _handlerFixture.FeedbackRepositoryMock.Verify(f => f.RemoveAsync(It.IsAny<int>()), Times.Once);
    }
}