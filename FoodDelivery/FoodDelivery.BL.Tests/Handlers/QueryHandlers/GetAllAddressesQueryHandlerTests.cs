using FluentAssertions;
using FoodDelivery.BL.Handlers.QueryHandlers.AddressQueryHandlers;
using FoodDelivery.BL.Queries.AddressQueries;
using FoodDelivery.BL.Tests.Handlers.CommandHandlers;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces.Base;
using FoodDelivery.Shared.Models.AddressModels;
using Moq;
using Range = Moq.Range;

namespace FoodDelivery.BL.Tests.Handlers.QueryHandlers;

public class GetAllAddressesQueryHandlerTests : IClassFixture<HandlerFixture>
{
    private readonly HandlerFixture _handlerFixture;
    
    public GetAllAddressesQueryHandlerTests(HandlerFixture handlerFixture)
    {
        _handlerFixture = handlerFixture;
    }
    
    
    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var page = 1;
        var pageSize = 5;
        var addressQueryObjectMock = new Mock<IQueryObject<AddressEntity>>();
        addressQueryObjectMock.Setup(a => a.ExecuteAsync().Result)
            .Returns(_handlerFixture.Addresses.Select(a => a.AddressEntity).Take(pageSize).ToList());
        
        _handlerFixture.MapperMock
            .Setup(m => m.Map<ICollection<AddressListModel>>(It.IsAny<ICollection<AddressEntity>>()))
            .Returns(_handlerFixture.Addresses.Select(a => a.AddressListModel).Take(pageSize).ToList);

        var request = new GetAllAddressesQuery(page, pageSize);
        var handler = new GetAllAddressesQueryHandler(_handlerFixture.MapperMock.Object, addressQueryObjectMock.Object);

        var actual = await handler.Handle(request, new CancellationToken());

        actual.Should().BeEquivalentTo(_handlerFixture.Addresses.Select(a => a.AddressListModel).Take(pageSize));
        
        addressQueryObjectMock.Verify(a => a.ExecuteAsync(), Times.Once);
        addressQueryObjectMock.Verify(a => a.Page(It.IsInRange(0, int.MaxValue, Range.Inclusive), It.IsInRange(0, int.MaxValue, Range.Inclusive)), Times.Once);
    }
}