using FoodDelivery.BL.Commands.AddressCommands;
using FoodDelivery.BL.Commands.FeedbackCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.AddressCommandHandlers;
using FoodDelivery.BL.Handlers.CommandHandlers.FeedbackCommandHandlers;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.AddressModels;
using FoodDelivery.Shared.Models.FeedbacksModels;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.FeedbackCommandHandlers;

public class CreateFeedbackCommandHandlerTests : IClassFixture<HandlerFixture>
{
    private readonly HandlerFixture _handlerFixture;
    
    public CreateFeedbackCommandHandlerTests(HandlerFixture handlerFixture)
    {
        _handlerFixture = handlerFixture;
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var entity = _handlerFixture.Feedbacks[0].FeedbackEntity;
        var createModel = _handlerFixture.Feedbacks[0].MealFeedbackCreateModel;
        var detailModel = _handlerFixture.Feedbacks[0].FeedbackDetailModel;
        
        _handlerFixture.FeedbackRepositoryMock.Setup(f => f.Insert(It.IsAny<FeedbackEntity>()));
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.FeedbackRepository)
            .Returns(_handlerFixture.FeedbackRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);
        _handlerFixture.MapperMock.Setup(m => m.Map<FeedbackEntity>(createModel))
            .Returns(entity);
        _handlerFixture.MapperMock.Setup(m => m.Map<FeedbackDetailModel>(entity))
            .Returns(detailModel);

        var request = new CreateFeedbackCommand(createModel);

        var handler = new CreateFeedbackCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object);
        
        Assert.True(await handler.Handle(request, new CancellationToken()) == detailModel);
    }
}