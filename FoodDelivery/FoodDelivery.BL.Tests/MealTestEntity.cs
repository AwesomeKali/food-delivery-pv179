using FoodDelivery.BL.Tests.Handlers;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.MealsModels;

namespace FoodDelivery.BL.Tests;

public class MealTestEntity
{
    public MealEntity MealEntity { get; set; }
    public MealCreateModel MealCreateModel { get; set; }
    public MealDetailModel MealDetailModel { get; set; }
    public MealListModel MealListModel { get; set; }
    public MealUpdateModel MealUpdateModel { get; set; }

    public List<OrderItemTestEntity> OrderItems
    {
        set
        {
            MealEntity.OrderItems = value.Select(o => o.OrderItemEntity).ToList();
        }
    }

    public List<FeedbackTestEntity> Feedbacks
    {
        set
        {
            MealEntity.Feedbacks = value.Select(f => f.FeedbackEntity).ToList();
            MealDetailModel.Feedbacks = value.Select(f => f.FeedbackListModel).ToList();
        }
    }

    public MealTestEntity(int id, string name, string description, double price, string mealType,
         RestaurantTestEntity restaurant)
    {
        MealEntity = new MealEntity
        {
            Id = id, Name = name, Description = description, Price = price, MealType = mealType,
            Restaurant = restaurant.RestaurantEntity, RestaurantId = restaurant.RestaurantEntity.Id
        };

        MealCreateModel = new MealCreateModel
        {
            Name = name, Description = description, Price = price, MealType = mealType,
            RestaurantId = restaurant.RestaurantEntity.Id
        };

        MealDetailModel = new MealDetailModel
        {
            Id = id, Name = name, Description = description, Price = price, MealType = mealType,
            Restaurant = restaurant.RestaurantDetailModel, RestaurantId = restaurant.RestaurantEntity.Id
        };

        MealListModel = new MealListModel
        {
            Id = id, Name = name, Description = description, Price = price, MealType = mealType,
            RestaurantId = restaurant.RestaurantEntity.Id
        };

        MealUpdateModel = new MealUpdateModel
        {
            Id = id, Name = name, Description = description, Price = price, MealType = mealType,
        };
    }
}