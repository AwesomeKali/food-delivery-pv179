using AutoMapper;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.OrderModels;
using FoodDelivery.Shared.Models.PaymentCardModels;

namespace FoodDelivery.BL.Profiles.OrderProfiles; 
internal class OrderDetailProfile : Profile
{
	public OrderDetailProfile()
	{
        CreateMap<OrderEntity, OrderDetailModel>().ReverseMap();
    }
}
