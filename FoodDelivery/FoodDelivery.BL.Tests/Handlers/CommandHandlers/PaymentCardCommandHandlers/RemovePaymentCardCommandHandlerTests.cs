using FoodDelivery.BL.Commands.OrderCommands;
using FoodDelivery.BL.Commands.PaymentCardCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.OrderCommandHandlers;
using FoodDelivery.BL.Handlers.CommandHandlers.PaymentCardCommandHandlers;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.PaymentCardCommandHandlers;

public class RemovePaymentCardCommandHandlerTests : IClassFixture<HandlerFixture>
{
    private readonly HandlerFixture _handlerFixture;
    
    public RemovePaymentCardCommandHandlerTests(HandlerFixture handlerFixture)
    {
        _handlerFixture = handlerFixture;
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var id = _handlerFixture.PaymentCards[0].PaymentCardEntity.Id;
        
        _handlerFixture.PaymentCardRepositoryMock.Setup(p => p.RemoveAsync(It.IsNotIn(id)))
            .Throws(new ArgumentException());
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.PaymentCardRepository)
            .Returns(_handlerFixture.PaymentCardRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);

        var request = new RemovePaymentCardCommand(id);

        var handler = new RemovePaymentCardCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object);
        
        Assert.True(await handler.Handle(request, new CancellationToken()));
        _handlerFixture.PaymentCardRepositoryMock.Verify(p => p.RemoveAsync(It.IsAny<int>()), Times.Once);
    }
}