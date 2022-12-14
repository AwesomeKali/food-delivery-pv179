namespace FoodDelivery.Shared.Models.FeedbacksModels;

public class MealFeedbackCreateModel
{
    public int UserId { get; set; }
    public int MealId { get; set; }
    public int Rating { get; set; }
    public string Description { get; set; }

}
