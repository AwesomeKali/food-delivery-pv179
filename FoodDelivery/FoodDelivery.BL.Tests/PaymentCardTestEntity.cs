using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.PaymentCardModels;

namespace FoodDelivery.BL.Tests;

public class PaymentCardTestEntity
{
    public PaymentCardEntity PaymentCardEntity { get; set; }
    public PaymentCardCreateModel PaymentCardCreateModel { get; set; }
    public PaymentCardDetailModel PaymentCardDetailModel { get; set; }
    public PaymentCardUpdateModel PaymentCardUpdateModel { get; set; }
    public PaymentCardListModel PaymentCardListModel { get; set; }

    public PaymentCardTestEntity(int id, string number, string expirationDate, string cardVerificationCode,
        UserTestEntity user)
    {
        PaymentCardEntity = new PaymentCardEntity
        {   
            Id = id, Number = number, ExpirationDate = expirationDate, CardVerificationCode = cardVerificationCode,
            User = user.CustomerEntity, UserId = user.CustomerEntity.Id
        };

        PaymentCardCreateModel = new PaymentCardCreateModel
        {
            Number = number, ExpirationDate = expirationDate, CardVerificationCode = cardVerificationCode,
            UserId = user.CustomerEntity.Id
        };

        PaymentCardDetailModel = new PaymentCardDetailModel
        {
            Id = id, Number = number, ExpirationDate = expirationDate, CardVerificationCode = cardVerificationCode,
            User = user.UserDetailModel, UserId = user.CustomerEntity.Id
        };

        PaymentCardUpdateModel = new PaymentCardUpdateModel
        {
            Id = id, Number = number, ExpirationDate = expirationDate, CardVerificationCode = cardVerificationCode,
            UserId = user.CustomerEntity.Id
        };

        PaymentCardListModel = new PaymentCardListModel
        {
            Id = id, Number = number, ExpirationDate = expirationDate, CardVerificationCode = cardVerificationCode,
            UserId = user.CustomerEntity.Id
        };
    }
}