using FoodDelivery.BL.Tests.Handlers;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.OrderItemsModels;
using FoodDelivery.Shared.Models.OrderModels;

namespace FoodDelivery.BL.Tests;

public class OrderItemTestEntity
{
    public OrderItemEntity OrderItemEntity { get; set; }
    public OrderItemCreateModel OrderItemCreateModel { get; set; }
    public OrderItemDetailModel OrderItemDetailModel { get; set; }
    public OrderItemListModel OrderItemListModel { get; set; }
    public OrderItemUpdateModel OrderItemUpdateModel { get; set; }

    public OrderItemTestEntity(int id, double unitPrice, int amount, OrderTestEntity order,
        MealTestEntity meal)
    {
        OrderItemEntity = new OrderItemEntity
        {
            Id = id, UnitPrice = unitPrice, Amount = amount,
            Order = order.OrderEntity, OrderId = order.OrderEntity.Id,
            Meal = meal.MealEntity, MealId = meal.MealEntity.Id
        };

        OrderItemCreateModel = new OrderItemCreateModel
        {
            OrderId = order.OrderEntity.Id,
            MealId = meal.MealEntity.Id
        };

        OrderItemDetailModel = new OrderItemDetailModel
        {
            Id = id, UnitPrice = unitPrice, Amount = amount,
            Order = order.OrderDetailModel, OrderId = order.OrderEntity.Id,
            Meal = meal.MealDetailModel, MealId = meal.MealEntity.Id
        };

        OrderItemUpdateModel = new OrderItemUpdateModel
        {
            Id = id, Amount = amount,
        };

        OrderItemListModel = new OrderItemListModel
        {
            Id = id, UnitPrice = unitPrice, Amount = amount,
            OrderId = order.OrderEntity.Id,
            MealId = meal.MealEntity.Id
        };
    }
}