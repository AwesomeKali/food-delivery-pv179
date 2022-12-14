using AutoMapper;
using FoodDelivery.BL.Handlers.QueryHandlers.Base;
using FoodDelivery.BL.Queries.MealQueries;
using FoodDelivery.BL.Queries.PaymentCardQueries;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces.Base;
using FoodDelivery.Shared.Models.MealsModels;
using FoodDelivery.Shared.Models.PaymentCardModels;
using MediatR;

namespace FoodDelivery.BL.Handlers.QueryHandlers.PaymentCardQueryHandlers;

internal class GetAllPaymentCardsQueryHandler : QueryHandler<GetAllPaymentCardsQuery, List<PaymentCardListModel>>, IRequestHandler<GetAllPaymentCardsQuery, List<PaymentCardListModel>>
{
    private readonly IQueryObject<PaymentCardEntity> _paymentCardQueryObject;

    public GetAllPaymentCardsQueryHandler(IMapper mapper, IQueryObject<PaymentCardEntity> paymentCardQueryObject) : base(mapper)
    {
        _paymentCardQueryObject = paymentCardQueryObject;
    }

    public override async Task<List<PaymentCardListModel>> Handle(GetAllPaymentCardsQuery request, CancellationToken cancellationToken)
    {
        if (request.Page > 0 && request.PageSize > 0)
            _paymentCardQueryObject.Page(request.Page, request.PageSize);

        var paymentCards = await _paymentCardQueryObject.ExecuteAsync();
        return _mapper.Map<ICollection<PaymentCardListModel>>(paymentCards).ToList();
    }
}
