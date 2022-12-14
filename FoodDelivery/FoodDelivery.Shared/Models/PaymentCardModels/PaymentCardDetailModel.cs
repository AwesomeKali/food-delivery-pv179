using FoodDelivery.Shared.Models.UserModels;

namespace FoodDelivery.Shared.Models.PaymentCardModels;

public class PaymentCardDetailModel
{
    public int Id { get; set; }
    public string Number { get; set; }
    public string ExpirationDate { get; set; }
    public string CardVerificationCode { get; set; }

    public int UserId { get; set; }
    public UserDetailModel User { get; set; }
}
