using AutoMapper;
using FoodDelivery.BL.Commands.FeedbackCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.Base;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.EFCore.UnitOfWork.Base;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using FoodDelivery.Shared.Models.FeedbacksModels;
using MediatR;

namespace FoodDelivery.BL.Handlers.CommandHandlers.FeedbackCommandHandlers;

public class CreateMealFeedbackCommandHandler : CommandHandler<CreateMealFeedbackCommand, FeedbackDetailModel>, IRequestHandler<CreateMealFeedbackCommand, FeedbackDetailModel>
{
    public CreateMealFeedbackCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper) : base(unitOfWorkProvider, mapper) { }

    public override async Task<FeedbackDetailModel> Handle(CreateMealFeedbackCommand request, CancellationToken cancellationToken)
    {
        var feedbackEntity = _mapper.Map<FeedbackEntity>(request.MealFeedbackCreateModel);

        var unitOfWork = _unitOfWorkProvider.Create();
        unitOfWork.FeedbackRepository.Insert(feedbackEntity);
        await unitOfWork.Commit();

        return _mapper.Map<FeedbackDetailModel>(feedbackEntity);
    }
}
