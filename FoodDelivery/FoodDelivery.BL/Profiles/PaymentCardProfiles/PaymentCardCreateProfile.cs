using AutoMapper;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.PaymentCardModels;

namespace FoodDelivery.BL.Profiles.PaymentCardProfiles; 
internal class PaymentCardCreateProfile : Profile
{
	public PaymentCardCreateProfile()
	{
        CreateMap<PaymentCardCreateModel, PaymentCardEntity>();
    }
}
