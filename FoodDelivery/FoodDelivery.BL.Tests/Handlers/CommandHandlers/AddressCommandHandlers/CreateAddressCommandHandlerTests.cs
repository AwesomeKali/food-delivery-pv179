using FoodDelivery.BL.Commands.AddressCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.AddressCommandHandlers;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.AddressModels;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.AddressCommandHandlers;

public class CreateAddressCommandHandlerTests : IClassFixture<HandlerFixture>
{
    private readonly HandlerFixture _handlerFixture;
    
    public CreateAddressCommandHandlerTests(HandlerFixture handlerFixture)
    {
        _handlerFixture = handlerFixture;
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var entity = _handlerFixture.Addresses[0].AddressEntity;
        var createModel = _handlerFixture.Addresses[0].AddressCreateModel;
        var detailModel = _handlerFixture.Addresses[0].AddressDetailModel;
        
        _handlerFixture.AddressRepositoryMock.Setup(a => a.Insert(It.IsAny<AddressEntity>()));
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.AddressRepository)
            .Returns(_handlerFixture.AddressRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);
        _handlerFixture.MapperMock.Setup(m => m.Map<AddressEntity>(createModel))
            .Returns(entity);
        _handlerFixture.MapperMock.Setup(m => m.Map<AddressDetailModel>(entity))
            .Returns(detailModel);

        var request = new CreateAddressCommand(createModel);

        var handler = new CreateAddressCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object);
        
        Assert.True(await handler.Handle(request, new CancellationToken()) == detailModel);
    }

}