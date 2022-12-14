using FoodDelivery.BL.Commands.MealCommands;
using FoodDelivery.BL.Commands.PaymentCardCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.MealCommandHandlers;
using FoodDelivery.BL.Handlers.CommandHandlers.PaymentCardCommandHandlers;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.MealsModels;
using FoodDelivery.Shared.Models.PaymentCardModels;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.PaymentCardCommandHandlers;

public class CreatePaymentCardCommandHandlerTests : IClassFixture<HandlerFixture>
{
    private readonly HandlerFixture _handlerFixture;
    
    public CreatePaymentCardCommandHandlerTests(HandlerFixture handlerFixture)
    {
        _handlerFixture = handlerFixture;
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var entity = _handlerFixture.PaymentCards[0].PaymentCardEntity;
        var createModel = _handlerFixture.PaymentCards[0].PaymentCardCreateModel;
        var detailModel = _handlerFixture.PaymentCards[0].PaymentCardDetailModel;
        
        _handlerFixture.PaymentCardRepositoryMock.Setup(m => m.Insert(It.IsAny<PaymentCardEntity>()));
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.PaymentCardRepository)
            .Returns(_handlerFixture.PaymentCardRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);
        _handlerFixture.MapperMock.Setup(m => m.Map<PaymentCardEntity>(createModel))
            .Returns(entity);
        _handlerFixture.MapperMock.Setup(m => m.Map<PaymentCardDetailModel>(entity))
            .Returns(detailModel);

        var request = new CreatePaymentCardCommand(createModel);

        var handler = new CreatePaymentCardCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object);
        
        Assert.True(await handler.Handle(request, new CancellationToken()) == detailModel);
    }
}