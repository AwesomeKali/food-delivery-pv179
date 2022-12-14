using FoodDelivery.DAL.Entities.Base;

namespace FoodDelivery.DAL.EFCore.Entities;

public class PaymentCardEntity : Entity
{
    public string Number { get; set; }
    public string ExpirationDate { get; set; }
    public string CardVerificationCode { get; set; }

    public int? UserId { get; set; }
    public UserEntity? User { get; set; }
}
