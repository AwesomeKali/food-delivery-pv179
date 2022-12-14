using AutoMapper;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.FeedbacksModels;
using FoodDelivery.Shared.Models.PaymentCardModels;

namespace FoodDelivery.BL.Profiles.PaymentCardProfiles; 
internal class PaymentCardDetailProfile : Profile
{
	public PaymentCardDetailProfile()
	{
        CreateMap<PaymentCardEntity, PaymentCardDetailModel>().ReverseMap();
    }
}
