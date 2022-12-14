using FoodDelivery.BL.Commands.FeedbackCommands;
using FoodDelivery.BL.Commands.MealCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.FeedbackCommandHandlers;
using FoodDelivery.BL.Handlers.CommandHandlers.MealCommandHandlers;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.FeedbacksModels;
using FoodDelivery.Shared.Models.MealsModels;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.MealCommandHandlers;

public class CreateMealCommandHandlerTests : IClassFixture<HandlerFixture>
{
    private readonly HandlerFixture _handlerFixture;
    
    public CreateMealCommandHandlerTests(HandlerFixture handlerFixture)
    {
        _handlerFixture = handlerFixture;
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var entity = _handlerFixture.Meals[0].MealEntity;
        var createModel = _handlerFixture.Meals[0].MealCreateModel;
        var detailModel = _handlerFixture.Meals[0].MealDetailModel;
        
        _handlerFixture.MealRepositoryMock.Setup(m => m.Insert(It.IsAny<MealEntity>()));
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.MealRepository)
            .Returns(_handlerFixture.MealRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);
        _handlerFixture.MapperMock.Setup(m => m.Map<MealEntity>(createModel))
            .Returns(entity);
        _handlerFixture.MapperMock.Setup(m => m.Map<MealDetailModel>(entity))
            .Returns(detailModel);

        var request = new CreateMealCommand(createModel);

        var handler = new CreateMealCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object);
        
        Assert.True(await handler.Handle(request, new CancellationToken()) == detailModel);
    }
}