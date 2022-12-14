using FoodDelivery.Shared.Models.PaymentCardModels;
using MediatR;

namespace FoodDelivery.BL.Queries.PaymentCardQueries;

public record GetAllPaymentCardsQuery(int Page = -1, int PageSize = -1) : IRequest<List<PaymentCardListModel>>;

