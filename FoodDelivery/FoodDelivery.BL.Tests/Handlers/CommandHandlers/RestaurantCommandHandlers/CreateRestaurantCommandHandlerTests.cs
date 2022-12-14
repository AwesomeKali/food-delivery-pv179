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

public class CreateRestaurantCommandHandlerTests : IClassFixture<HandlerFixture>
{
    private readonly HandlerFixture _handlerFixture;
    
    public CreateRestaurantCommandHandlerTests(HandlerFixture handlerFixture)
    {
        _handlerFixture = handlerFixture;
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var entity = _handlerFixture.Restaurants[0].RestaurantEntity;
        var createModel = _handlerFixture.Restaurants[0].RestaurantCreateModel;
        var detailModel = _handlerFixture.Restaurants[0].RestaurantDetailModel;
        
        _handlerFixture.RestaurantRepositoryMock.Setup(m => m.Insert(It.IsAny<RestaurantEntity>()));
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.RestaurantRepository)
            .Returns(_handlerFixture.RestaurantRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);
        _handlerFixture.MapperMock.Setup(m => m.Map<RestaurantEntity>(createModel))
            .Returns(entity);
        _handlerFixture.MapperMock.Setup(m => m.Map<RestaurantDetailModel>(entity))
            .Returns(detailModel);

        var request = new CreateRestaurantCommand(createModel);

        var handler = new CreateRestaurantCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object);
        
        Assert.True(await handler.Handle(request, new CancellationToken()) == detailModel);
    }
}