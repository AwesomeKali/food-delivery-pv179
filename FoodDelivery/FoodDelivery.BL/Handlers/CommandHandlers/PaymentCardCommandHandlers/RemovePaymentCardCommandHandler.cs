using AutoMapper;
using FoodDelivery.BL.Commands.MealCommands;
using FoodDelivery.BL.Commands.PaymentCardCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.Base;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.EFCore.UnitOfWork.Base;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using MediatR;

namespace FoodDelivery.BL.Handlers.CommandHandlers.PaymentCardCommandHandlers;

public class RemovePaymentCardCommandHandler : CommandHandler<RemovePaymentCardCommand, bool>, IRequestHandler<RemovePaymentCardCommand, bool>
{
    public RemovePaymentCardCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper) : base(unitOfWorkProvider, mapper) { }

    public override async Task<bool> Handle(RemovePaymentCardCommand request, CancellationToken cancellationToken)
    {
        var unitOfWork = _unitOfWorkProvider.Create();
        await unitOfWork.PaymentCardRepository.RemoveAsync(request.Id);
        await unitOfWork.Commit();
        return true;
    }
}
