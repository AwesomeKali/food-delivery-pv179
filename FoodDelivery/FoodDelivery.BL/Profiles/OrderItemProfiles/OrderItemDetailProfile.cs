using AutoMapper;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.OrderItemsModels;
using FoodDelivery.Shared.Models.PaymentCardModels;

namespace FoodDelivery.BL.Profiles.OrderItemProfiles;
internal class OrderItemDetailProfile : Profile
{
	public OrderItemDetailProfile()
	{
        CreateMap<OrderItemEntity, OrderItemDetailModel>().ReverseMap();
    }
}
