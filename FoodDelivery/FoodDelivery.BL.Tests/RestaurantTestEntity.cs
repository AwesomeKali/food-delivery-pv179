using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.RestaurantModels;

namespace FoodDelivery.BL.Tests;

public class RestaurantTestEntity
{
    public RestaurantEntity RestaurantEntity { get; set; }
    public RestaurantCreateModel RestaurantCreateModel { get; set; }
    public RestaurantDetailModel RestaurantDetailModel { get; set; }
    public RestaurantListModel RestaurantListModel { get; set; }
    public RestaurantUpdateModel RestaurantUpdateModel { get; set; }

    public List<MealTestEntity> Meals
    {
        set
        {
            RestaurantEntity.Meals = value.Select(m => m.MealEntity).ToList();
            RestaurantDetailModel.Meals = value.Select(m => m.MealListModel).ToList();
            RestaurantListModel.Meals = value.Select(m => m.MealListModel).ToList();
        }
    }

    public List<FeedbackTestEntity> Feedbacks
    {
        set
        {
            RestaurantEntity.Feedbacks = value.Select(f => f.FeedbackEntity).ToList();
            RestaurantDetailModel.Feedbacks = value.Select(f => f.FeedbackListModel).ToList();
            RestaurantListModel.Feedbacks = value.Select(f => f.FeedbackListModel).ToList();
        }
    }

    public RestaurantTestEntity(int id, string name)
    {
        RestaurantEntity = new RestaurantEntity
        {
            Id = id, Name = name,
        };

        RestaurantCreateModel = new RestaurantCreateModel
        {
            Name = name
        };

        RestaurantDetailModel = new RestaurantDetailModel
        {
            Id = id, Name = name,
        };

        RestaurantListModel = new RestaurantListModel
        {
            Id = id, Name = name
        };

        RestaurantUpdateModel = new RestaurantUpdateModel
        {
            Id = id, Name = name
        };
    }
}