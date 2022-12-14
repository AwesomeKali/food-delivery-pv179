using FoodDelivery.BL.Commands.AddressCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.AddressCommandHandlers;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.AddressModels;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.AddressCommandHanlers;

public class RemoveAddressCommandHandlerTests : IClassFixture<HandlerFixture>
{
    private readonly HandlerFixture _handlerFixture;
    
    public RemoveAddressCommandHandlerTests(HandlerFixture handlerFixture)
    {
        _handlerFixture = handlerFixture;
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var id = _handlerFixture.Addresses[0].AddressEntity.Id;
        
        _handlerFixture.AddressRepositoryMock.Setup(a => a.RemoveAsync(It.IsNotIn(id)))
            .Throws(new ArgumentException());
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.AddressRepository)
            .Returns(_handlerFixture.AddressRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);

        var request = new RemoveAddressCommand(id);

        var handler = new RemoveAddressCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object);
        
        Assert.True(await handler.Handle(request, new CancellationToken()));
        _handlerFixture.AddressRepositoryMock.Verify(a => a.RemoveAsync(It.IsAny<int>()), Times.Once);
    }
}