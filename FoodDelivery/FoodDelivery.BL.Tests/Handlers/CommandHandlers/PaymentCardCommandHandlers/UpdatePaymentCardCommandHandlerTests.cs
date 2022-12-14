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

public class UpdatePaymentCardCommandHandlerTests : IClassFixture<HandlerFixture>
{
    private readonly HandlerFixture _handlerFixture;
    
    public UpdatePaymentCardCommandHandlerTests(HandlerFixture handlerFixture)
    {
        _handlerFixture = handlerFixture;
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var entity = _handlerFixture.PaymentCards[0].PaymentCardEntity;
        var updateModel = _handlerFixture.PaymentCards[0].PaymentCardUpdateModel;
        var detailModel = _handlerFixture.PaymentCards[0].PaymentCardDetailModel;
        
        _handlerFixture.PaymentCardRepositoryMock.Setup(p => p.Update(It.IsNotIn(entity)))
            .Throws(new ArgumentException());
        _handlerFixture.MapperMock.Setup(m => m.Map<PaymentCardEntity>(It.IsAny<PaymentCardUpdateModel>()))
            .Returns(entity);
        _handlerFixture.MapperMock.Setup(m => m.Map<PaymentCardDetailModel>(It.IsAny<PaymentCardEntity>()))
            .Returns(detailModel);
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.PaymentCardRepository)
            .Returns(_handlerFixture.PaymentCardRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);

        var request = new UpdatePaymentCardCommand(updateModel);

        var handler = new UpdatePaymentCardCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object);
        
        Assert.True(await handler.Handle(request, new CancellationToken()) == detailModel);
        _handlerFixture.PaymentCardRepositoryMock.Verify(p => p.Update(It.IsAny<PaymentCardEntity>()), Times.Once);
    }
}