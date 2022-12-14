using AutoMapper;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.MealsModels;
using FoodDelivery.Shared.Models.PaymentCardModels;

namespace FoodDelivery.BL.Profiles.PaymentCardProfiles;
internal class PaymentCardUpdateProfile : Profile
{
	public PaymentCardUpdateProfile()
	{
        CreateMap<PaymentCardUpdateModel, PaymentCardEntity>();
    }
}
