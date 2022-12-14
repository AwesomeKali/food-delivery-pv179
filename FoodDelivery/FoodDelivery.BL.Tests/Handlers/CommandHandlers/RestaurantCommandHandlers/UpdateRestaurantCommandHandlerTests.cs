using FoodDelivery.BL.Commands.MealCommands;
using FoodDelivery.BL.Commands.RestaurantCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.MealCommandHandlers;
using FoodDelivery.BL.Handlers.CommandHandlers.RestaurantCommandHandlers;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.MealsModels;
using FoodDelivery.Shared.Models.RestaurantModels;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.RestaurantCommandHandlers;

public class UpdateRestaurantCommandHandlerTests : IClassFixture<HandlerFixture>
{
    private readonly HandlerFixture _handlerFixture;
    
    public UpdateRestaurantCommandHandlerTests(HandlerFixture handlerFixture)
    {
        _handlerFixture = handlerFixture;
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var entity = _handlerFixture.Restaurants[0].RestaurantEntity;
        var updateModel = _handlerFixture.Restaurants[0].RestaurantUpdateModel;
        var detailModel = _handlerFixture.Restaurants[0].RestaurantDetailModel;
        
        _handlerFixture.RestaurantRepositoryMock.Setup(r => r.Update(It.IsNotIn(entity)))
            .Throws(new ArgumentException());
        _handlerFixture.MapperMock.Setup(m => m.Map<RestaurantEntity>(It.IsAny<RestaurantUpdateModel>()))
            .Returns(entity);
        _handlerFixture.MapperMock.Setup(m => m.Map<RestaurantDetailModel>(It.IsAny<RestaurantEntity>()))
            .Returns(detailModel);
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.RestaurantRepository)
            .Returns(_handlerFixture.RestaurantRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);

        var request = new UpdateRestaurantCommand(updateModel);

        var handler = new UpdateRestaurantCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object);
        
        Assert.True(await handler.Handle(request, new CancellationToken()) == detailModel);
        _handlerFixture.RestaurantRepositoryMock.Verify(r => r.Update(It.IsAny<RestaurantEntity>()), Times.Once);
    }
}