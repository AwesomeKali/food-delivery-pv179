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

public class UpdateMealCommandHandlerTests : IClassFixture<HandlerFixture>
{
    private readonly HandlerFixture _handlerFixture;
    
    public UpdateMealCommandHandlerTests(HandlerFixture handlerFixture)
    {
        _handlerFixture = handlerFixture;
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var entity = _handlerFixture.Meals[0].MealEntity;
        var updateModel = _handlerFixture.Meals[0].MealUpdateModel;
        var detailModel = _handlerFixture.Meals[0].MealDetailModel;
        
        _handlerFixture.MealRepositoryMock.Setup(f => f.Update(It.IsNotIn(entity)))
            .Throws(new ArgumentException());
        _handlerFixture.MapperMock.Setup(m => m.Map<MealEntity>(It.IsAny<MealUpdateModel>()))
            .Returns(entity);
        _handlerFixture.MapperMock.Setup(m => m.Map<MealDetailModel>(It.IsAny<MealEntity>()))
            .Returns(detailModel);
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.MealRepository)
            .Returns(_handlerFixture.MealRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);

        var request = new UpdateMealCommand(updateModel);

        var handler = new UpdateMealCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object);
        
        Assert.True(await handler.Handle(request, new CancellationToken()) == detailModel);
        _handlerFixture.MealRepositoryMock.Verify(a => a.Update(It.IsAny<MealEntity>()), Times.Once);
    }
}