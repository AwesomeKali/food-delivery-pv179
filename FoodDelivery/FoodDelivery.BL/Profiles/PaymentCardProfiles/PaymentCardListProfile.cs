using AutoMapper;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.RestaurantModels;

namespace FoodDelivery.BL.Profiles.PaymentCardProfiles; 
internal class PaymentCardListProfile : Profile
{
	public PaymentCardListProfile()
	{
        CreateMap<RestaurantEntity, RestaurantListModel>();
    }
}
