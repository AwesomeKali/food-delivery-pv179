using FoodDelivery.BL.Commands.AddressCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.AddressCommandHandlers;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.AddressModels;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.AddressCommandHanlers;

public class UpdateAddressCommandHandlerTests : IClassFixture<HandlerFixture>
{
    private readonly HandlerFixture _handlerFixture;
    
    public UpdateAddressCommandHandlerTests(HandlerFixture handlerFixture)
    {
        _handlerFixture = handlerFixture;
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var entity = _handlerFixture.Addresses[0].AddressEntity;
        var updateModel = _handlerFixture.Addresses[0].AddressUpdateModel;
        var detailModel = _handlerFixture.Addresses[0].AddressDetailModel;
        
        _handlerFixture.AddressRepositoryMock.Setup(a => a.Update(It.IsNotIn(entity)))
            .Throws(new ArgumentException());
        _handlerFixture.MapperMock.Setup(m => m.Map<AddressEntity>(It.IsAny<AddressUpdateModel>()))
            .Returns(entity);
        _handlerFixture.MapperMock.Setup(m => m.Map<AddressDetailModel>(It.IsAny<AddressEntity>()))
            .Returns(detailModel);
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.AddressRepository)
            .Returns(_handlerFixture.AddressRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);

        var request = new UpdateOrderAddressCommand(updateModel);

        var handler = new UpdateAddressCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object);
        
        Assert.True(await handler.Handle(request, new CancellationToken()) == detailModel);
        _handlerFixture.AddressRepositoryMock.Verify(a => a.Update(It.IsAny<AddressEntity>()), Times.Once);
    }
}