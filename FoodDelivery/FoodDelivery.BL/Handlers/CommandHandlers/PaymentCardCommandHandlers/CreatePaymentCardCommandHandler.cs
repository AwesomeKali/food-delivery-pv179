using AutoMapper;
using FoodDelivery.BL.Commands.PaymentCardCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.Base;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.EFCore.UnitOfWork.Base;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using FoodDelivery.Shared.Models.MealsModels;
using FoodDelivery.Shared.Models.PaymentCardModels;
using MediatR;

namespace FoodDelivery.BL.Handlers.CommandHandlers.PaymentCardCommandHandlers;

public class CreatePaymentCardCommandHandler : CommandHandler<CreatePaymentCardCommand, PaymentCardDetailModel>, IRequestHandler<CreatePaymentCardCommand, PaymentCardDetailModel>
{
    public CreatePaymentCardCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper) : base(unitOfWorkProvider, mapper) { }

    public override async Task<PaymentCardDetailModel> Handle(CreatePaymentCardCommand request, CancellationToken cancellationToken)
    {
        var paymentCardEntity = _mapper.Map<PaymentCardEntity>(request.PaymentCardCreateModel);

        var unitOfWork = _unitOfWorkProvider.Create();
        unitOfWork.PaymentCardRepository.Insert(paymentCardEntity);
        await unitOfWork.Commit();

        return _mapper.Map<PaymentCardDetailModel>(paymentCardEntity);
    }
}
