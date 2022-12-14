using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.FeedbacksModels;

namespace FoodDelivery.BL.Tests;

public class FeedbackTestEntity
{
    public FeedbackEntity FeedbackEntity { get; set; }
    public MealFeedbackCreateModel MealFeedbackCreateModel { get; set; }
    public FeedbackDetailModel FeedbackDetailModel { get; set; }
    public FeedbackListModel FeedbackListModel { get; set; }

    public FeedbackTestEntity(int id, int rating, string description, MealTestEntity meal, RestaurantTestEntity restaurant,
        UserTestEntity user)
    {
        FeedbackEntity = new FeedbackEntity
        { 
            Id = id, Rating = rating, Description = description,
            Restaurant = restaurant.RestaurantEntity, RestaurantId = restaurant.RestaurantEntity.Id,
            User = user.CustomerEntity, UserId = user.CustomerEntity.Id,
            Meal = meal.MealEntity, MealId = meal.MealEntity.Id
        };
        MealFeedbackCreateModel = new MealFeedbackCreateModel
        {
            Rating = rating, Description = description,
            MealId = meal.MealEntity.Id,
            UserId = user.CustomerEntity.Id
        };
        FeedbackDetailModel = new FeedbackDetailModel
        {
            Id = id, Rating = rating, Description = description,
            Meal = meal.MealDetailModel, MealId = meal.MealEntity.Id,
            Restaurant = restaurant.RestaurantDetailModel, RestaurantId = restaurant.RestaurantEntity.Id,
            User = user.UserDetailModel, UserId = user.CustomerEntity.Id
        };
        
        FeedbackListModel = new FeedbackListModel
        {
            Id = id, Rating = rating, Description = description,
            MealId = meal.MealEntity.Id,
            RestaurantId = restaurant.RestaurantEntity.Id,
            UserId = user.CustomerEntity.Id
        };
    }
}